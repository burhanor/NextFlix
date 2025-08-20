using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Category.Queries.CategoryIsExist
{


	public class CategoryIsExistQueryHandler(IUow uow) : IsExistHandler<Domain.Entities.Category, CategoryIsExistQueryRequest>(uow)
	{
	}
}
