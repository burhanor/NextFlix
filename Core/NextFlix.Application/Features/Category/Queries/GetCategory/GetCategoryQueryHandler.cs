using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.Redis;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Category.Queries.GetCategory
{


	public class GetCategoryQueryHandler(IUow uow, IMapper mapper, IRedisService redisService) : GetByIdHandler<Domain.Entities.Category, GetCategoryQueryRequest, GetCategoryQueryResponse>(uow, mapper, redisService, RedisPrefix.Category)
	{

	}
}
