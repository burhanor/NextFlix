using NextFlix.Application.Dto.CastDtos;

namespace NextFlix.API.Models
{
	public class CastModel: CastDto
	{
		public IFormFile? File { get; set; }
	}
}
