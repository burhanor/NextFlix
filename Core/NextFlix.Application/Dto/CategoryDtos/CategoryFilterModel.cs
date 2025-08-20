using NextFlix.Application.Models;
using NextFlix.Domain.Enums;

namespace NextFlix.Application.Dto.CategoryDtos
{
	public class CategoryFilterModel : FilterModel
	{
		public string? Name { get; set; }
		public string? Slug { get; set; }
		public Status[]? Status { get; set; }
	}
}
