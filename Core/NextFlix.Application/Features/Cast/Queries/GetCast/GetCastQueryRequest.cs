using MediatR;
using NextFlix.Shared.Interfaces;

namespace NextFlix.Application.Features.Cast.Queries.GetCast
{
	public class GetCastQueryRequest(int id) : IRequest<GetCastQueryResponse>, IId
	{
		public int Id { get; set; } = id;
	}
}
