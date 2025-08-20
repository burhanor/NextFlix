using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.Redis;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Features.Source.Queries.GetSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextFlix.Application.Features.Source.Queries.GetSource
{
	

	public class GetSourceQueryHandler(IUow uow, IMapper mapper, IRedisService redisService) : GetByIdHandler<Domain.Entities.Source, GetSourceQueryRequest, GetSourceQueryResponse>(uow, mapper, redisService, RedisPrefix.Source)
	{

	}
}
