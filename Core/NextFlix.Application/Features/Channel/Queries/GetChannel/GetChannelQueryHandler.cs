using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.Redis;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Channel.Queries.GetChannel
{
	public class GetChannelQueryHandler(IUow uow, IMapper mapper, IRedisService redisService) : GetByIdHandler<Domain.Entities.Channel, GetChannelQueryRequest, GetChannelQueryResponse>(uow, mapper, redisService, RedisPrefix.Channel)
	{

	}
}
