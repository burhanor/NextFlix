using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Source.Commands.DeleteSource
{
	public class DeleteSourceCommandRequest : DeleteRequest
	{
		public DeleteSourceCommandRequest() : base()
		{

		}
		public DeleteSourceCommandRequest(int id) : base(id)
		{
		}
		public DeleteSourceCommandRequest(List<int> ids) : base(ids)
		{
		}
	}
}
