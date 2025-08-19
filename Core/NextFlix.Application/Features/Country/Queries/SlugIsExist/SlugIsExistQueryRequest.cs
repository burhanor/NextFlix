using NextFlix.Application.Interfaces;
using NextFlix.Domain.Enums;

namespace NextFlix.Application.Features.Country.Queries.SlugIsExist
{
	public class SlugIsExistQueryRequest(string slug,Status? status) : ISlugIsExistRequest
	{
		public string Slug { get; set; } = slug;
		public Status? Status { get; set; } = status;
	}
}
