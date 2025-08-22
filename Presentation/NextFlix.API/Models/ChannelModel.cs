using NextFlix.Application.Dto.ChannelDtos;

namespace NextFlix.API.Models
{
	public class ChannelModel:ChannelDto
	{
		public IFormFile? File { get; set; }
	}
}
