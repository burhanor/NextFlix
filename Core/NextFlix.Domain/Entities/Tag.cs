using NextFlix.Domain.Concretes;
using NextFlix.Domain.Enums;
using NextFlix.Domain.Interfaces;

namespace NextFlix.Domain.Entities
{
	public class Tag:EntityBase,IStatus,ISlug
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }
		
		public virtual ICollection<MovieTag> MovieTags { get; set; } = [];
	}
}
