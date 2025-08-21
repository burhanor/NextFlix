using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Movie.Commands.WatchMovie
{
	public class WatchMovieCommandRequest(int movieId) : IRequestContainer<WatchMovieCommandResponse>
	{
		public int MovieId { get; set; } = movieId;
	}
}
