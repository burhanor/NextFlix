using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.MeiliSearch;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Models;
using NextFlix.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextFlix.Application.Features.Movie.Queries.GetMovies
{
	public class GetMoviesQueryHandler(IUow uow, IMapper mapper,IMeiliSearchService meiliSearchService) : BaseHandler<Domain.Entities.Movie>(uow, mapper), IRequestHandler<GetMoviesQueryRequest, PaginationContainer<GetMoviesQueryResponse>>
	{
		public async Task<PaginationContainer<GetMoviesQueryResponse>> Handle(GetMoviesQueryRequest request, CancellationToken cancellationToken)
		{
			List<int> ids = await meiliSearchService.SearchMoviesAsync(request);
			PaginationContainer<GetMoviesQueryResponse> response = new PaginationContainer<GetMoviesQueryResponse>
			{
				Items = new List<GetMoviesQueryResponse>(),
				PageNumber = 1,
				PageSize = 10,
				TotalCount = ids.Count
			};
			if (ids?.Count>0)
			{
				response.Items = ids.Select(m => new GetMoviesQueryResponse
				{
					Id = m
				}).ToList();
			}

			return response;
		}
	}
}
