using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.User.Queries.UserIsExist
{
	public class UserIsExistQueryRequest(int id) : IRequestContainer<bool>
	{
		public int Id { get; set; } = id;
	}
}
