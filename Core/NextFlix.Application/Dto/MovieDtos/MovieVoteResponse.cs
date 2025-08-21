using NextFlix.Domain.Enums;

namespace NextFlix.Application.Dto.MovieDtos
{
	public class MovieVoteResponse
	{
		public VoteType Vote { get; set; }
		public int VoteCount { get; set; }
	}
}
