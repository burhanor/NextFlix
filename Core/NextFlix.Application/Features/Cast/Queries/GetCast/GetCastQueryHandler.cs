using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.Redis;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Cast.Queries.GetCast
{
	
	public class GetCastQueryHandler(IUow uow, IMapper mapper, IRedisService redisService) : GetByIdHandler<Domain.Entities.Cast, GetCastQueryRequest, GetCastQueryResponse>(uow, mapper, redisService, RedisPrefix.Cast)
	{

	}
}
