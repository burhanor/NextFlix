using NextFlix.Application.Dto.MovieDtos;

namespace NextFlix.Application.Interfaces
{
	public interface IMovieHelper
	{
		Task<Domain.Entities.Movie> ToMovieEntity(MovieDto movieDto, CancellationToken cancellationToken = default);
		Task<MovieResponse> FillRelations(MovieResponse movie);
	}
}
