using NextFlix.Domain.Concretes;
using NextFlix.Domain.Enums;

namespace NextFlix.Domain.Entities
{
	public class Category : EntityBase
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }
		public virtual ICollection<MovieCategory> MovieCategories { get; set; } = [];
	}
}
