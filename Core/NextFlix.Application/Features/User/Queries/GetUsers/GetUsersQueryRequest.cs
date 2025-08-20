using MediatR;
using NextFlix.Application.Dto.UserDtos;
using NextFlix.Application.Models;

namespace NextFlix.Application.Features.User.Queries.GetUsers
{
	public class GetUsersQueryRequest : UserFilterModel, IRequest<PaginationContainer<GetUsersQueryResponse>>
	{
	}
}
