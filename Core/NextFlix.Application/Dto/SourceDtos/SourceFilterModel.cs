using NextFlix.Application.Models;
using NextFlix.Domain.Enums;

namespace NextFlix.Application.Dto.SourceDtos
{
	public class SourceFilterModel:FilterModel
	{
		public string? Title { get; set; }
		public Status[]? Status { get; set; }
		public SourceType[]? SourceType { get; set; }
	}
}
