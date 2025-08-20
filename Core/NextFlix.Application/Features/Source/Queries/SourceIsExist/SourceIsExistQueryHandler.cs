using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Source.Queries.SourceIsExist
{
	public class SourceIsExistQueryHandler(IUow uow) : IsExistHandler<Domain.Entities.Source, SourceIsExistQueryRequest>(uow)
	{
	}
}
