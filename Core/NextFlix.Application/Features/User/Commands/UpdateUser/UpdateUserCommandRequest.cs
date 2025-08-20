using NextFlix.Application.Dto.ImageDto;
using NextFlix.Application.Dto.UserDtos;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.User.Commands.UpdateUser
{
	public class UpdateUserCommandRequest : UserDto, IRequestContainer<UpdateUserCommandResponse>
	{
		public ImageDto? AvatarImage { get; set; }
		public int Id { get; set; }
	}
}
