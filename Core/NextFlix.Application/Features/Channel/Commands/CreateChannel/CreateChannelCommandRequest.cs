using NextFlix.Application.Dto.ChannelDtos;
using NextFlix.Application.Dto.ImageDto;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Channel.Commands.CreateChannel
{
	public class CreateChannelCommandRequest : ChannelDto, IRequestContainer<CreateChannelCommandResponse>
	{
		public ImageDto? LogoImage { get; set; }
	}
}
