using NextFlix.Domain.Enums;

namespace NextFlix.Application.Dto.ChannelDtos
{
	public class ChannelDto
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }
	}
}
