using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.MeiliSearch;
using NextFlix.Application.Abstraction.Interfaces.Redis;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Models;
using NextFlix.Shared.Models;

namespace NextFlix.Application.Features.Movie.Queries.GetMovies
{
	public class GetMoviesQueryHandler(IUow uow, IMapper mapper,IMeiliSearchService meiliSearchService,IRedisService redisService) : BaseHandler<Domain.Entities.Movie>(uow, mapper), IRequestHandler<GetMoviesQueryRequest, PaginationContainer<GetMoviesQueryResponse>>
	{
		public async Task<PaginationContainer<GetMoviesQueryResponse>> Handle(GetMoviesQueryRequest request, CancellationToken cancellationToken)
		{
			MeiliSearchResponse searchResponse = await meiliSearchService.SearchMoviesAsync(request);
			PaginationContainer<GetMoviesQueryResponse> response = new PaginationContainer<GetMoviesQueryResponse>
			{
				PageNumber = request.PageNumber??1,
				PageSize = request.PageSize??20,
				TotalCount = searchResponse.TotalCount
			};
			if (searchResponse?.MovieIds?.Count>0)
			{
				response.Items = await redisService.HashGetAsync<GetMoviesQueryResponse>(RedisPrefix.Movie.ToString(), searchResponse.MovieIds)??[];
				response.TotalCount = searchResponse.TotalCount;
			}
			return response;
		}
	}
}
