using NextFlix.Application.Interfaces;
using NextFlix.Domain.Enums;

namespace NextFlix.Application.Features.Movie.Commands.VoteMovie
{
	public class VoteMovieCommandRequest:IRequestContainer<VoteMovieCommandResponse>
	{
		public VoteMovieCommandRequest(int movieId, VoteType vote)
		{
			MovieId = movieId;
			Vote = vote;
		}

		public int MovieId { get; set; }
		public VoteType Vote { get; set; }
	}
}
