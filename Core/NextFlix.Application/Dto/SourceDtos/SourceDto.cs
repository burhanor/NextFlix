using NextFlix.Domain.Enums;

namespace NextFlix.Application.Dto.SourceDtos
{
	public class SourceDto
	{
		public string Title { get; set; }
		public Status Status { get; set; }
		public SourceType SourceType { get; set; }
	}
}
