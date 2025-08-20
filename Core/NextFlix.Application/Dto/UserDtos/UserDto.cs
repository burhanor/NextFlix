using NextFlix.Domain.Enums;

namespace NextFlix.Application.Dto.UserDtos
{
	public class UserDto
	{
		public string Nickname { get; set; }
		public string EmailAddress { get; set; }
		public string Password { get; set; }
		public UserType UserType { get; set; }
		public string Slug { get; set; }
		public bool IsActive { get; set; }
	}
}
