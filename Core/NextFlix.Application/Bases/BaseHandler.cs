using AutoMapper;
using Microsoft.AspNetCore.Http;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Extensions;
using NextFlix.Domain.Interfaces;

namespace NextFlix.Application.Bases
{
	public class BaseHandler<T> where T : class, IEntityBase, new()
	{
		public readonly IUow uow;
		public readonly IHttpContextAccessor httpContextAccessor;
		public readonly IMapper mapper;
		public readonly int userId = 0;
		public readonly string ipAddress = string.Empty;
		public readonly IReadRepository<T> readRepository;
		public readonly IWriteRepository<T> writeRepository;
		public readonly string languageCode = "tr";
		public readonly IRabbitMqService RabbitMqService;

		public BaseHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService) : base()
		{
			this.uow = uow;
			this.httpContextAccessor = httpContextAccessor;
			this.mapper = mapper;
			readRepository = uow.GetReadRepository<T>();
			writeRepository = uow.GetWriteRepository<T>();
			ipAddress = httpContextAccessor.GetIpAddress();
			userId = httpContextAccessor.GetUserId();
			RabbitMqService = rabbitMqService;
		}
		public BaseHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : base()
		{
			this.uow = uow;
			this.mapper = mapper;
			readRepository = uow.GetReadRepository<T>();
			writeRepository = uow.GetWriteRepository<T>();
			RabbitMqService = rabbitMqService;
		}

		public BaseHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base()
		{
			this.uow = uow;
			this.httpContextAccessor = httpContextAccessor;
			this.mapper = mapper;
			readRepository = uow.GetReadRepository<T>();
			writeRepository = uow.GetWriteRepository<T>();
			ipAddress = httpContextAccessor.GetIpAddress();
			userId = httpContextAccessor.GetUserId();
		}

		public BaseHandler(IUow uow, IMapper mapper) : base()
		{
			this.uow = uow;
			this.mapper = mapper;
			readRepository = uow.GetReadRepository<T>();
			writeRepository = uow.GetWriteRepository<T>();
		}

		public BaseHandler(IUow uow) : base()
		{
			this.uow = uow;
			readRepository = uow.GetReadRepository<T>();
			writeRepository = uow.GetWriteRepository<T>();
		}
	}
}
