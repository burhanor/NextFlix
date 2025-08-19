using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Country.Queries.GetCountries
{
	public class GetCountriesQueryHandler(IUow uow, IMapper mapper) : BaseHandler<Domain.Entities.Country>(uow, mapper), IRequestHandler<GetCountriesQueryRequest, PaginationContainer<GetCountriesQueryResponse>>
	{
		public async Task<PaginationContainer<GetCountriesQueryResponse>> Handle(GetCountriesQueryRequest request, CancellationToken cancellationToken)
		{
			IQueryable<Domain.Entities.Country> query = readRepository.Query();
			if (!string.IsNullOrEmpty(request.Name))
			{
				query = query.Where(x => x.Name.Contains(request.Name));
			}
			if (!string.IsNullOrEmpty(request.Slug))
			{
				query = query.Where(x => x.Slug.Contains(request.Slug));
			}
			if (request.Status?.Length>0)
			{
				query = query.Where(x => request.Status.Contains(x.Status));
			}
			int totalCount = await readRepository.CountAsync(query, cancellationToken);
			if (request.PageNumber.HasValue)
				query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.GetValueOrDefault(10)).Take(request.PageSize.GetValueOrDefault(10));
			else if (request.PageSize.HasValue)
				query = query.Take(request.PageSize.Value);

			PaginationContainer<GetCountriesQueryResponse> response = new()
			{
				Items = mapper.Map<List<GetCountriesQueryResponse>>(query),
				TotalCount = totalCount,
				PageNumber = request.PageNumber ?? 1,
				PageSize = request.PageSize ?? int.MaxValue,
			};

			IList<Domain.Entities.Country> countries = await readRepository.ToListAsync(query, cancellationToken);
			response.Items = mapper.Map<List<GetCountriesQueryResponse>>(countries);

			return response;
		}
	}
}
