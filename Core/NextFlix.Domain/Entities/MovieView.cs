using NextFlix.Domain.Concretes;

namespace NextFlix.Domain.Entities
{
	public class MovieView:EntityBase
	{
		public int MovieId { get; set; }
		public string IpAddress { get; set; }
		public DateTime ViewDate { get; set; }
	}
}
