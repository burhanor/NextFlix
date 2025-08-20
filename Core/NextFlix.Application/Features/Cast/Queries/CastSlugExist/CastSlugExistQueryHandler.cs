using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Cast.Queries.CastSlugExist
{
	public class CastSlugExistQueryHandler(IUow uow) : SlugExistHandler<Domain.Entities.Cast, CastSlugExistQueryRequest>(uow)
	{

	}
}
