using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Auth.Commands.Register
{
	public class RegisterCommandRequest : IRequestContainer<RegisterCommandResponse>
	{
		public string EmailAddress { get; set; }
		public string Password { get; set; }
		public string Nickname { get; set; }
	}
}
