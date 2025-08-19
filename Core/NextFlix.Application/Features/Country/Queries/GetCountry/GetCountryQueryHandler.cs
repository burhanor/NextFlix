using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.Redis;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Country.Queries.GetCountry
{
	public class GetCountryQueryHandler(IUow uow, IMapper mapper,IRedisService redisService) : GetByIdHandler<Domain.Entities.Country,GetCountryQueryRequest,GetCountryQueryResponse>(uow,mapper,redisService,RedisPrefix.Country)
	{
		
	}
}
