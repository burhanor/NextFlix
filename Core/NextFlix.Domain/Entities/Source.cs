using NextFlix.Domain.Concretes;
using NextFlix.Domain.Enums;

namespace NextFlix.Domain.Entities
{
	public class Source:EntityBase
	{
		public string Title { get; set; }
		public Status Status { get; set; }
		public SourceType SourceType { get; set; }
	}
}
