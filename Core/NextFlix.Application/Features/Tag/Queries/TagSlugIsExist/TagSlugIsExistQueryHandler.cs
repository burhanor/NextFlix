using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Tag.Queries.TagSlugIsExist
{
	public class TagSlugIsExistQueryHandler(IUow uow) : SlugExistHandler<Domain.Entities.Tag, TagSlugIsExistQueryRequest>(uow)
	{

	}
	
}
