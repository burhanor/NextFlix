using NextFlix.Application.Interfaces;
using NextFlix.Domain.Enums;

namespace NextFlix.Application.Features.Cast.Queries.CastSlugExist
{
	public class CastSlugExistQueryRequest(string slug, Status? status) : ISlugIsExistRequest
	{
		public string Slug { get; set; } = slug;
		public Status? Status { get; set; } = status;
	}
}
