using NextFlix.Application.Dto.ChannelDtos;
using NextFlix.Application.Dto.ImageDto;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Channel.Commands.UpdateChannel
{
	public class UpdateChannelCommandRequest : ChannelDto, IRequestContainer<UpdateChannelCommandResponse>
	{
		public ImageDto? LogoImage { get; set; }
		public int Id { get; set; }
	}
}
