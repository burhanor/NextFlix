using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.Redis;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.User.Queries.GetUser
{


	public class GetUserQueryHandler(IUow uow, IMapper mapper, IRedisService redisService) : GetByIdHandler<Domain.Entities.User, GetUserQueryRequest, GetUserQueryResponse>(uow, mapper, redisService, RedisPrefix.User)
	{

	}
}
