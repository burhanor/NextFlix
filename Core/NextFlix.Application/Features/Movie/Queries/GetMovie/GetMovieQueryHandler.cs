using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Dto.MovieDtos;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Movie.Queries.GetMovie
{
	public class GetMovieQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IMovieHelper movieHelper) : BaseHandler<Domain.Entities.Movie>(uow, httpContextAccessor, mapper), IRequestHandler<GetMovieQueryRequest, GetMovieQueryResponse?>
	{
		public async Task<GetMovieQueryResponse?> Handle(GetMovieQueryRequest request, CancellationToken cancellationToken)
		{
			Domain.Entities.Movie? movie = await readRepository.GetAsync(request.MovieId,cancellationToken:cancellationToken);
			if (movie is null)
				return null;
			MovieResponse movieResponse = mapper.Map<MovieResponse>(movie);
			movieResponse = await movieHelper.FillRelations(movieResponse);
			GetMovieQueryResponse response = mapper.Map<GetMovieQueryResponse>(movieResponse);
			return response; 
		}
		
	}
}
