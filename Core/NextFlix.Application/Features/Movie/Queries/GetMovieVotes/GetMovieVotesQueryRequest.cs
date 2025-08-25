using MediatR;

namespace NextFlix.Application.Features.Movie.Queries.GetMovieVotes
{
	public class GetMovieVotesQueryRequest(int movieId) : IRequest<List<GetMovieVotesQueryResponse>>
	{
		public int MovieId { get; set; } = movieId;
	}
}
