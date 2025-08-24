using MediatR;
using NextFlix.Application.Dto.MovieDtos;
using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Movie.Queries.GetMovies
{
	public class GetMoviesQueryRequest:MovieFilterModel,IRequest<PaginationContainer<GetMoviesQueryResponse>>
	{
	}
}
