using NextFlix.Domain.Concretes;

namespace NextFlix.Domain.Entities
{
	public class MovieCountry : EntityBase
	{
		public int MovieId { get; set; }
		public int CountryId { get; set; }
		public byte DisplayOrder { get; set; }
		public virtual Movie Movie { get; set; }
		public virtual Country Country { get; set; }
	}
}
