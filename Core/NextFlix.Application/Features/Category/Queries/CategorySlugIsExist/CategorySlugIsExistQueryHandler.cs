using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Category.Queries.CategorySlugIsExist
{

	public class CategorySlugIsExistQueryHandler(IUow uow) : SlugExistHandler<Domain.Entities.Category, CategorySlugIsExistQueryRequest>(uow)
	{

	}
}
