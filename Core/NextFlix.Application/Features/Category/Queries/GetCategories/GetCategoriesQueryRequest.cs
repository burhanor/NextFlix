using MediatR;
using NextFlix.Application.Dto.CategoryDtos;
using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Category.Queries.GetCategories
{
	
	public class GetCategoriesQueryRequest : CategoryFilterModel, IRequest<PaginationContainer<GetCategoriesQueryResponse>>
	{
	}
}
