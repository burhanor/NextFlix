using NextFlix.Shared.Models;
using System.Security.Claims;

namespace NextFlix.Application.Abstraction.Interfaces.Token
{
	public interface ITokenService
	{
		string GenerateAccessToken(UserModel user);
		string GenerateRefreshToken();
		ClaimsPrincipal? GetClaimsPrincipalFromToken(string token);
	}
}
