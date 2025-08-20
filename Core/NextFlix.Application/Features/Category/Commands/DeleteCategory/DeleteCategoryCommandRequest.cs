using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Category.Commands.DeleteCategory
{

	public class DeleteCategoryCommandRequest : DeleteRequest
	{
		public DeleteCategoryCommandRequest() : base()
		{

		}
		public DeleteCategoryCommandRequest(int id) : base(id)
		{
		}
		public DeleteCategoryCommandRequest(List<int> ids) : base(ids)
		{
		}
	}
}
