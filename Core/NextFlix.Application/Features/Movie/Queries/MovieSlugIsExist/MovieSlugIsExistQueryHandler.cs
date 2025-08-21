using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Movie.Queries.MovieSlugIsExist
{

	public class MovieSlugIsExistQueryHandler(IUow uow) : SlugExistHandler<Domain.Entities.Movie, MovieSlugIsExistQueryRequest>(uow)
	{

	}
}
