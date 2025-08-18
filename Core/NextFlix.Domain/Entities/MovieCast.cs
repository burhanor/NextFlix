using NextFlix.Domain.Concretes;

namespace NextFlix.Domain.Entities
{
	public class MovieCast:EntityBase
	{
		public int MovieId { get; set; }
		public int CastId { get; set; }
		public byte DisplayOrder { get; set; }
		public virtual Movie Movie { get; set; }
		public virtual Cast Cast { get; set; }
	}
}
