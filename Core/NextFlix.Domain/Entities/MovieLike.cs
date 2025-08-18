using NextFlix.Domain.Concretes;
using NextFlix.Domain.Enums;

namespace NextFlix.Domain.Entities
{
	public class MovieLike:EntityBase
	{
		public int MovieId { get; set; }
		public string IpAddress { get; set; }
		public VoteType Vote { get; set; }
		public DateTime VoteDate { get; set; }
	}
}
