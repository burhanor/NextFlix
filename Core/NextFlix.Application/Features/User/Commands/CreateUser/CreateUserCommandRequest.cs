using NextFlix.Application.Dto.ImageDto;
using NextFlix.Application.Dto.UserDtos;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.User.Commands.CreateUser
{
	public class CreateUserCommandRequest : UserDto, IRequestContainer<CreateUserCommandResponse>
	{
		public ImageDto? AvatarImage { get; set; }
	}
}
