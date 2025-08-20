using NextFlix.Domain.Enums;

namespace NextFlix.Application.Dto.TagDtos
{
	public class TagDto
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }
	}
}
