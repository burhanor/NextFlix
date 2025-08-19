using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Country.Queries.CountryIsExist
{
	public class CountryIsExistQueryHandler(IUow uow) : IsExistHandler<Domain.Entities.Country,CountryIsExistQueryRequest>(uow) { 
	}
}
