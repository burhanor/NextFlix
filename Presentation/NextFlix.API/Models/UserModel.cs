using NextFlix.Application.Dto.UserDtos;

namespace NextFlix.API.Models
{
	public class UserModel:UserDto
	{
		public IFormFile? File { get; set; }
	}
}
