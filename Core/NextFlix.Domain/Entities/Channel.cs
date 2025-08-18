using NextFlix.Domain.Concretes;
using NextFlix.Domain.Enums;

namespace NextFlix.Domain.Entities
{
	public class Channel:EntityBase
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }
		public string? Logo { get; set; }
		public virtual ICollection<MovieChannel> MovieChannels { get; set; } = [];
	}
}
