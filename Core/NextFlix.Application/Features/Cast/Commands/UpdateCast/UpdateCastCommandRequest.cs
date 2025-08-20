using NextFlix.Application.Dto.CastDtos;
using NextFlix.Application.Dto.ImageDto;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Cast.Commands.UpdateCast
{
	public class UpdateCastCommandRequest : CastDto, IRequestContainer<UpdateCastCommandResponse>
	{
		public ImageDto? AvatarImage { get; set; }
		public int Id { get; set; }
	}
}
