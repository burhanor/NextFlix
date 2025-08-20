using NextFlix.Application.Interfaces;
using NextFlix.Domain.Enums;

namespace NextFlix.Application.Features.Category.Queries.CategoryIsExist
{
	
	public class CategoryIsExistQueryRequest(int id, Status? status) : IIsExistRequest
	{
		public int Id { get; set; } = id;
		public Status? Status { get; set; } = status;
	}
}
