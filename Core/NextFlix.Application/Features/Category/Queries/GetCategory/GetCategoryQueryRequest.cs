using MediatR;
using NextFlix.Shared.Interfaces;

namespace NextFlix.Application.Features.Category.Queries.GetCategory
{

	public class GetCategoryQueryRequest(int id) : IRequest<GetCategoryQueryResponse>, IId
	{
		public int Id { get; set; } = id;
	}
}
