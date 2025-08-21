using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Movie.Queries.MovieIsExist
{
	public class MovieIsExistQueryHandler(IUow uow) : IsExistHandler<Domain.Entities.Movie, MovieIsExistQueryRequest>(uow)
	{
	}
}
