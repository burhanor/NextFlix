using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.User.Queries.UserSlugIsExist
{
	public class UserSlugIsExistQueryRequest(string slug):IRequestContainer<bool>
	{
		public string Slug { get; set; } = slug;
	}
}
