using NextFlix.Domain.Enums;

namespace NextFlix.Application.Dto.SourceDtos
{
	public class SourceResponse
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public Status Status { get; set; }
		public SourceType SourceType { get; set; }
	}
}
