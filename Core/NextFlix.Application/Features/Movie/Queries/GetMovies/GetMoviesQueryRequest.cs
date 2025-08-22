using MediatR;
using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Movie.Queries.GetMovies
{
	public class GetMoviesQueryRequest:IRequest<PaginationContainer<GetMoviesQueryResponse>>
	{
	}
}
