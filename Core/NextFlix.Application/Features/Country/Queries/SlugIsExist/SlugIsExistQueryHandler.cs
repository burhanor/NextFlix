using MediatR;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Features.Country.Queries.CountryIsExist;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Country.Queries.SlugIsExist
{
	public class SlugIsExistQueryHandler(IUow uow) : SlugExistHandler<Domain.Entities.Country, SlugIsExistQueryRequest>(uow)
	{
		
	}
}
