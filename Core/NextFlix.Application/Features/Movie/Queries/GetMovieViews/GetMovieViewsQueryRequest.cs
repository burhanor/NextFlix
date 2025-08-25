using MediatR;

namespace NextFlix.Application.Features.Movie.Queries.GetMovieViews
{
	public class GetMovieViewsQueryRequest(int movieId) : IRequest<GetMovieViewsQueryResponse>
	{
		public int MovieId { get; set; } = movieId;
	}
}
