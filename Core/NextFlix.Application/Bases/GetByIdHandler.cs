using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.Redis;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Domain.Interfaces;
using NextFlix.Shared.Interfaces;

namespace NextFlix.Application.Bases
{
	public class GetByIdHandler<T,TRequest, TResponse>(IUow uow, IMapper mapper, IRedisService redisService,RedisPrefix prefix) : IRequestHandler<TRequest, TResponse?>
		where T :class,IEntityBase,new()
		where TRequest : IRequest<TResponse?>, IId
		where TResponse : class,new()
	{
		public readonly IReadRepository<T> readRepository = uow.GetReadRepository<T>();
		
		public async Task<TResponse?> Handle(TRequest request, CancellationToken cancellationToken)
		{
			TResponse? response = await redisService.GetAsync<TResponse>($"{prefix}:", request.Id.ToString());
			if (response != null)
				return response;
			T? entity = await readRepository.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
			if (entity == null)
				return null;
			response = mapper.Map<TResponse>(entity);
			await redisService.StringSetAsync($"{prefix}:{request.Id}", response);
			return response;

		}
	}
}
