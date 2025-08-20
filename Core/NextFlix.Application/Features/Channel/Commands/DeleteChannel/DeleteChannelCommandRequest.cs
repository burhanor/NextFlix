using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Channel.Commands.DeleteChannel
{
	public class DeleteChannelCommandRequest : DeleteRequest
	{
		public DeleteChannelCommandRequest() : base()
		{

		}
		public DeleteChannelCommandRequest(int id) : base(id)
		{
		}
		public DeleteChannelCommandRequest(List<int> ids) : base(ids)
		{
		}
	}
}
