using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Tag.Queries.TagIsExist
{
	
	public class TagIsExistQueryHandler(IUow uow) : IsExistHandler<Domain.Entities.Tag, TagIsExistQueryRequest>(uow)
	{
	}
}
