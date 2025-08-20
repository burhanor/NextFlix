using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Auth.Commands.Login
{
	public class LoginCommandRequest:IRequestContainer<LoginCommandResponse>
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
