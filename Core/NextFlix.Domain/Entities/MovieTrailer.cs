using NextFlix.Domain.Concretes;

namespace NextFlix.Domain.Entities
{
	public class MovieTrailer:EntityBase
	{
		public int MovieId { get; set; }
		public string TrailerLink { get; set; }
		public byte DisplayOrder { get; set; }
	}
}
