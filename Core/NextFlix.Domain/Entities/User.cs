using NextFlix.Domain.Concretes;
using NextFlix.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextFlix.Domain.Entities
{
	public class User:EntityBase
	{
		public string Nickname { get; set; }
		public string EmailAddress { get; set; }
		public string Password { get; set; }
		public UserType UserType { get; set; }
		public string? Avatar { get; set; }
		public string Slug { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
