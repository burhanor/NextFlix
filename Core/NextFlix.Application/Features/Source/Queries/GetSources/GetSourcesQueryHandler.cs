using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Source.Queries.GetSources
{
	

	public class GetSourcesQueryHandler(IUow uow, IMapper mapper) : BaseHandler<Domain.Entities.Source>(uow, mapper), IRequestHandler<GetSourcesQueryRequest, PaginationContainer<GetSourcesQueryResponse>>
	{
		public async Task<PaginationContainer<GetSourcesQueryResponse>> Handle(GetSourcesQueryRequest request, CancellationToken cancellationToken)
		{
			IQueryable<Domain.Entities.Source> query = readRepository.Query();
			if (!string.IsNullOrEmpty(request.Title))
			{
				query = query.Where(x => x.Title.Contains(request.Title));
			}
			if (request.Status?.Length > 0)
			{
				query = query.Where(x => request.Status.Contains(x.Status));
			}
			if (request.SourceType?.Length > 0)
			{
				query = query.Where(x => request.SourceType.Contains(x.SourceType));
			}
			int totalCount = await readRepository.CountAsync(query, cancellationToken);
			if (request.PageNumber.HasValue)
				query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.GetValueOrDefault(10)).Take(request.PageSize.GetValueOrDefault(10));
			else if (request.PageSize.HasValue)
				query = query.Take(request.PageSize.Value);

			PaginationContainer<GetSourcesQueryResponse> response = new()
			{
				Items = mapper.Map<List<GetSourcesQueryResponse>>(query),
				TotalCount = totalCount,
				PageNumber = request.PageNumber ?? 1,
				PageSize = request.PageSize ?? totalCount,
			};

			IList<Domain.Entities.Source> sources = await readRepository.ToListAsync(query, cancellationToken);
			response.Items = mapper.Map<List<GetSourcesQueryResponse>>(sources);

			return response;
		}
	}
}
