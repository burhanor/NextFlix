using NextFlix.Application.Dto.CastDtos;
using NextFlix.Application.Dto.ImageDto;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Cast.Commands.CreateCast
{
	public class CreateCastCommandRequest : CastDto, IRequestContainer<CreateCastCommandResponse>
	{
		public ImageDto? AvatarImage { get; set; }
	}
}
