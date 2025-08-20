namespace NextFlix.Infrastructure.Token
{
	public class TokenModel
	{
		public string Audience { get; set; } 
		public string Issuer { get; set; } 
		public string SecretKey { get; set; }
		public int TokenValidtyInMinutes { get; set; }
		public int RefreshTokenValidtyInDays { get; set; }
	}
}
