using NextFlix.Application.Dto.MovieDtos;

namespace NextFlix.Application.Interfaces
{
	public interface IMovieHelper
	{
		Task<Domain.Entities.Movie> ToMovieEntity(MovieDto movieDto, CancellationToken cancellationToken = default);
		Task<MovieResponse> FillRelations(MovieResponse movie);
		Task<int> GetMovieViews(int movieId, CancellationToken cancellationToken = default);
		Task<List<MovieVoteResponse>?> GetMovieVotes(int movieId, CancellationToken cancellationToken = default);
	}
}
