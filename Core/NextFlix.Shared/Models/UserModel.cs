namespace NextFlix.Shared.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		public string? Avatar { get; set; }
		public string Nickname { get; set; }
		public string EmailAddress { get; set; }
		public string Password { get; set; }
		public int UserType { get; set; }
		public string Slug { get; set; }
	}
}
