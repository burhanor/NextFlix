using Microsoft.AspNetCore.Http;

namespace NextFlix.Application.Extensions
{
	public static class HttpContextAccessorExtension
	{
		public static int GetUserId(this IHttpContextAccessor accessor)
		{
			if (accessor.HttpContext != null && accessor.HttpContext.User.Identity != null)
				return accessor.HttpContext.User.Identity.IsAuthenticated ? Convert.ToInt32(accessor.HttpContext.User.FindFirst("id")?.Value) : 0;
			return 0;
		}
		public static string GetIpAddress(this IHttpContextAccessor accessor)
		{
			return accessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
		}
		public static string GetNickname(this IHttpContextAccessor accessor)
		{
			return accessor.HttpContext?.User.Identity?.Name ?? string.Empty;
		}
	}
}
