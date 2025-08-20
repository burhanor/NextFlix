using NextFlix.Domain.Concretes;
using NextFlix.Domain.Enums;
using NextFlix.Domain.Interfaces;

namespace NextFlix.Domain.Entities
{
	public class User:EntityBase,ISlug
	{
		public string Nickname { get; set; }
		public string EmailAddress { get; set; }
		public string Password { get; set; }
		public UserType UserType { get; set; }
		public string? Avatar { get; set; }
		public string Slug { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedDate { get; set; }
		public virtual ICollection<Movie> Movies { get; set; } = [];
		public virtual ICollection<Login> Logins { get; set; } = [];
		public virtual ICollection<Log> Logs { get; set; } = [];
	}
}
