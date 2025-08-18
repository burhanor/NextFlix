using NextFlix.Domain.Concretes;
using NextFlix.Domain.Enums;

namespace NextFlix.Domain.Entities
{
	public class Cast:EntityBase
	{
		public string Name { get; set; }
		public string? Avatar { get; set; }
		public string Slug { get; set; } 
		public Status Status { get; set; }
		public DateTime? BirthDate { get; set; }
		public string? Biography { get; set; }
		public int CountryId { get; set; }
		public CastType CastType { get; set; }
		public Gender Gender { get; set; }
		public virtual Country Country { get; set; }
		public virtual ICollection<MovieCast> MovieCasts { get; set; } = [];

	}
}
