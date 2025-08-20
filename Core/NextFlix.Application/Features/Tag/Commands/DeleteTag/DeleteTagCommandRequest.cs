using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Tag.Commands.DeleteTag
{
	

	public class DeleteTagCommandRequest : DeleteRequest
	{
		public DeleteTagCommandRequest() : base()
		{

		}
		public DeleteTagCommandRequest(int id) : base(id)
		{
		}
		public DeleteTagCommandRequest(List<int> ids) : base(ids)
		{
		}
	}
}
