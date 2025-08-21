using MediatR;

namespace NextFlix.Application.Features.Movie.Queries.GetMovie
{
	public class GetMovieQueryRequest(int movieId) : IRequest<GetMovieQueryResponse?>
	{
		public int MovieId { get; set; } = movieId;
	}
}
