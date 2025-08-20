using MediatR;
using NextFlix.Shared.Interfaces;

namespace NextFlix.Application.Features.Channel.Queries.GetChannel
{
	public class GetChannelQueryRequest(int id) : IRequest<GetChannelQueryResponse>, IId
	{
		public int Id { get; set; } = id;
	}
}
