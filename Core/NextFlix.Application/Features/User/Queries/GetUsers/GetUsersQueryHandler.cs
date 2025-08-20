using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Models;

namespace NextFlix.Application.Features.User.Queries.GetUsers
{
	

	public class GetUsersQueryHandler(IUow uow, IMapper mapper) : BaseHandler<Domain.Entities.User>(uow, mapper), IRequestHandler<GetUsersQueryRequest, PaginationContainer<GetUsersQueryResponse>>
	{
		public async Task<PaginationContainer<GetUsersQueryResponse>> Handle(GetUsersQueryRequest request, CancellationToken cancellationToken)
		{
			IQueryable<Domain.Entities.User> query = readRepository.Query();
			if (!string.IsNullOrEmpty(request.Nickname))
			{
				query = query.Where(x => x.Nickname.Contains(request.Nickname));
			}
			if (!string.IsNullOrEmpty(request.EmailAddress))
			{
				query = query.Where(x => x.EmailAddress.Contains(request.EmailAddress));
			}
			if (!string.IsNullOrEmpty(request.Slug))
			{
				query = query.Where(x => x.Slug.Contains(request.Slug));
			}
			if (request.UserType?.Length > 0)
			{
				query = query.Where(x => request.UserType.Contains(x.UserType));
			}
			if(request.IsActive.HasValue)
				query = query.Where(x => x.IsActive == request.IsActive.Value);
			if(request.MinCreatedDate.HasValue)
				query = query.Where(x => x.CreatedDate >= request.MinCreatedDate.Value);
			if (request.MaxCreatedDate.HasValue)
				query = query.Where(x => x.CreatedDate <= request.MaxCreatedDate.Value);
			int totalCount = await readRepository.CountAsync(query, cancellationToken);
			if (request.PageNumber.HasValue)
				query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.GetValueOrDefault(10)).Take(request.PageSize.GetValueOrDefault(10));
			else if (request.PageSize.HasValue)
				query = query.Take(request.PageSize.Value);

			PaginationContainer<GetUsersQueryResponse> response = new()
			{
				Items = mapper.Map<List<GetUsersQueryResponse>>(query),
				TotalCount = totalCount,
				PageNumber = request.PageNumber ?? 1,
				PageSize = request.PageSize ?? totalCount,
			};

			IList<Domain.Entities.User> users = await readRepository.ToListAsync(query, cancellationToken);
			response.Items = mapper.Map<List<GetUsersQueryResponse>>(users);

			return response;
		}
	}
}
