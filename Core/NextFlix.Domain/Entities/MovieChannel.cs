using NextFlix.Domain.Concretes;

namespace NextFlix.Domain.Entities
{
	public class MovieChannel : EntityBase
	{
		public int MovieId { get; set; }
		public int ChannelId { get; set; }
		public byte DisplayOrder { get; set; }
		public virtual Movie Movie { get; set; }
		public virtual Channel Channel { get; set; }
	}
}
