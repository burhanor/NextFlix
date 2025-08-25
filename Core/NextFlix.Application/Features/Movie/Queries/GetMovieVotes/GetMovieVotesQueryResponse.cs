using NextFlix.Domain.Enums;

namespace NextFlix.Application.Features.Movie.Queries.GetMovieVotes
{
	public class GetMovieVotesQueryResponse
	{
		public VoteType Vote { get; set; }
		public int VoteCount { get; set; }
	}
}
