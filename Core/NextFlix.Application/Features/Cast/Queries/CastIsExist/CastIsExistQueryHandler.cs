using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Cast.Queries.CastIsExist
{
	

	public class CastIsExistQueryHandler(IUow uow) : IsExistHandler<Domain.Entities.Cast, CastIsExistQueryRequest>(uow)
	{
	}
}
