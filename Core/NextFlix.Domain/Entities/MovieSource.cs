using NextFlix.Domain.Concretes;

namespace NextFlix.Domain.Entities
{
	public class MovieSource:EntityBase
	{
		public int SourceId { get; set; }
		public int MovieId { get; set; }
		public string Link { get; set; }
		public byte DisplayOrder { get; set; }
		public virtual Movie Movie { get; set; }
		public virtual Source Source { get; set; }
	}
}
