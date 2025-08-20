using NextFlix.Application.Models;

namespace NextFlix.Application.Features.User.Commands.DeleteUser
{
	
	public class DeleteUserCommandRequest : DeleteRequest
	{
		public DeleteUserCommandRequest() : base()
		{

		}
		public DeleteUserCommandRequest(int id) : base(id)
		{
		}
		public DeleteUserCommandRequest(List<int> ids) : base(ids)
		{
		}
	}
}
