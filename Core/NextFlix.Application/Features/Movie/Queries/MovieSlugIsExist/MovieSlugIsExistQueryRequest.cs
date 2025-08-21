using NextFlix.Application.Interfaces;
using NextFlix.Domain.Enums;

namespace NextFlix.Application.Features.Movie.Queries.MovieSlugIsExist
{
	public class MovieSlugIsExistQueryRequest(string slug, Status? status) : ISlugIsExistRequest
	{
		public string Slug { get; set; } = slug;
		public Status? Status { get; set; } = status;
	}
}
