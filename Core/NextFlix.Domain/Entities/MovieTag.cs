using NextFlix.Domain.Concretes;

namespace NextFlix.Domain.Entities
{
	internal class MovieTag : EntityBase
	{
		public int MovieId { get; set; }
		public int TagId { get; set; }
		public byte DisplayOrder { get; set; }
	}
}
