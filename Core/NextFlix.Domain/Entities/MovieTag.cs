using NextFlix.Domain.Concretes;

namespace NextFlix.Domain.Entities
{
	public class MovieTag : EntityBase
	{
		public int MovieId { get; set; }
		public int TagId { get; set; }
		public byte DisplayOrder { get; set; }
		public virtual Movie Movie { get; set; }
		public virtual Tag Tag { get; set; }
	}
}
