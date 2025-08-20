using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.Redis;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Tag.Queries.GetTag
{

	public class GetTagQueryHandler(IUow uow, IMapper mapper, IRedisService redisService) : GetByIdHandler<Domain.Entities.Tag, GetTagQueryRequest, GetTagQueryResponse>(uow, mapper, redisService, RedisPrefix.Tag)
	{

	}
}
