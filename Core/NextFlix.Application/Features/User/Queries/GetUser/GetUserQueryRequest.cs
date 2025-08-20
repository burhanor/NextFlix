using MediatR;
using NextFlix.Shared.Interfaces;

namespace NextFlix.Application.Features.User.Queries.GetUser
{
	public class GetUserQueryRequest(int id) : IRequest<GetUserQueryResponse>, IId
	{
		public int Id { get; set; } = id;
	}
}
