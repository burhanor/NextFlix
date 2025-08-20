using NextFlix.Application.Models;
using NextFlix.Domain.Enums;

namespace NextFlix.Application.Dto.UserDtos
{
	public class UserFilterModel:FilterModel
	{
		public string? Nickname { get; set; }
		public string? EmailAddress { get; set; }
		public string? Password { get; set; }
		public UserType[]? UserType { get; set; }
		public string? Avatar { get; set; }
		public string? Slug { get; set; }
		public bool? IsActive { get; set; }
		public DateTime? MinCreatedDate { get; set; }
		public DateTime? MaxCreatedDate { get; set; }
	}
}
