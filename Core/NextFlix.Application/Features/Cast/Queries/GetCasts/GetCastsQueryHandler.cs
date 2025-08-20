using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Cast.Queries.GetCasts
{
	


	public class GetCastsQueryHandler(IUow uow, IMapper mapper) : BaseHandler<Domain.Entities.Cast>(uow, mapper), IRequestHandler<GetCastsQueryRequest, PaginationContainer<GetCastsQueryResponse>>
	{
		public async Task<PaginationContainer<GetCastsQueryResponse>> Handle(GetCastsQueryRequest request, CancellationToken cancellationToken)
		{
			IQueryable<Domain.Entities.Cast> query = readRepository.Query();
			if (!string.IsNullOrEmpty(request.Name))
			{
				query = query.Where(x => x.Name.Contains(request.Name));
			}
			if (!string.IsNullOrEmpty(request.Slug))
			{
				query = query.Where(x => x.Slug.Contains(request.Slug));
			}
			if (request.Status?.Length > 0)
			{
				query = query.Where(x => request.Status.Contains(x.Status));
			}
			if(request.CountryId.HasValue)
			{
				query = query.Where(x => x.CountryId == request.CountryId.Value);
			}
			if(request.Gender?.Length>0)
			{
				query = query.Where(x => request.Gender.Contains(x.Gender));
			}
			if (request.CastType?.Length > 0)
			{
				query = query.Where(x => request.CastType.Contains(x.CastType));
			}
			if (request.MinBirthDate.HasValue)
			{
				query = query.Where(x => x.BirthDate >= request.MinBirthDate.Value);
			}
			if (request.MaxBirthDate.HasValue)
			{
				query = query.Where(x => x.BirthDate <= request.MaxBirthDate.Value);
			}
			if (!string.IsNullOrEmpty(request.Biography))
			{
				query = query.Where(x => x.Biography.Contains(request.Biography));
			}
			int totalCount = await readRepository.CountAsync(query, cancellationToken);
			if (request.PageNumber.HasValue)
				query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.GetValueOrDefault(10)).Take(request.PageSize.GetValueOrDefault(10));
			else if (request.PageSize.HasValue)
				query = query.Take(request.PageSize.Value);

			PaginationContainer<GetCastsQueryResponse> response = new()
			{
				Items = mapper.Map<List<GetCastsQueryResponse>>(query),
				TotalCount = totalCount,
				PageNumber = request.PageNumber ?? 1,
				PageSize = request.PageSize ?? totalCount,
			};

			IList<Domain.Entities.Cast> casts = await readRepository.ToListAsync(query, cancellationToken);
			response.Items = mapper.Map<List<GetCastsQueryResponse>>(casts);

			return response;
		}
	}
}
