using MediatR;

namespace NextFlix.Application.Features.Movie.Queries.GetMovieBySlug
{
	public class GetMovieBySlugQueryRequest(string slug) : IRequest<GetMovieBySlugQueryResponse?>
	{
		public string Slug { get; set; } = slug;
	}
}
