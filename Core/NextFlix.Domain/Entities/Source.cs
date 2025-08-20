using NextFlix.Domain.Concretes;
using NextFlix.Domain.Enums;
using NextFlix.Domain.Interfaces;

namespace NextFlix.Domain.Entities
{
	public class Source:EntityBase,IStatus
	{
		public string Title { get; set; }
		public Status Status { get; set; }
		public SourceType SourceType { get; set; }
		public virtual ICollection<MovieSource> MovieSources { get; set; } = [];
	}
}
