using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Country.Queries.SlugIsExist
{
	public class SlugIsExistQueryHandler(IUow uow) : SlugExistHandler<Domain.Entities.Country, SlugIsExistQueryRequest>(uow)
	{
		
	}
}
