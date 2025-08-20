using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Cast.Commands.DeleteCast
{

	public class DeleteCastCommandRequest : DeleteRequest
	{
		public DeleteCastCommandRequest() : base()
		{

		}
		public DeleteCastCommandRequest(int id) : base(id)
		{
		}
		public DeleteCastCommandRequest(List<int> ids) : base(ids)
		{
		}
	}
}
