using NextFlix.Domain.Concretes;

namespace NextFlix.Domain.Entities
{
	public class Login:EntityBase
	{
		public int UserId { get; set; }
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public string IpAddress { get; set; }
		public DateTime LoginDate { get; set; }
		public virtual User User { get; set; }
	}
}
