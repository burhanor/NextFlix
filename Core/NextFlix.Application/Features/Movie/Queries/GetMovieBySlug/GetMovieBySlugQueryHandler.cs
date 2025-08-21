using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Dto.MovieDtos;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Movie.Queries.GetMovieBySlug
{
	public class GetMovieBySlugQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IMovieHelper movieHelper) : BaseHandler<Domain.Entities.Movie>(uow, httpContextAccessor, mapper), IRequestHandler<GetMovieBySlugQueryRequest, GetMovieBySlugQueryResponse?>
	{
		public async Task<GetMovieBySlugQueryResponse?> Handle(GetMovieBySlugQueryRequest request, CancellationToken cancellationToken)
		{
			Domain.Entities.Movie? movie = await readRepository.GetAsync(m=>m.Slug==request.Slug, cancellationToken: cancellationToken);
			if (movie is null)
				return null;
			MovieResponse movieResponse = mapper.Map<MovieResponse>(movie);
			movieResponse = await movieHelper.FillRelations(movieResponse);
			GetMovieBySlugQueryResponse response = mapper.Map<GetMovieBySlugQueryResponse>(movieResponse);
			return response;
		}

	}
}
