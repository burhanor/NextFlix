using MediatR;
using NextFlix.Application.Dto.ChannelDtos;
using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Channel.Queries.GetChannels
{
	public class GetChannelsQueryRequest : ChannelFilterModel, IRequest<PaginationContainer<GetChannelsQueryResponse>>
	{
	}
}
