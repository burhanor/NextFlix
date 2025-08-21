using NextFlix.Application.Dto.ChannelDtos;

namespace NextFlix.Application.Dto.MovieDtos
{
	public class MovieChannelResponse:ChannelResponse
	{
		public byte DisplayOrder { get; set; }
	}
}
