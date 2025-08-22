using NextFlix.Application.Dto.CountryDtos;

namespace NextFlix.API.Models
{
	public class CountryModel:CountryDto
	{
		public IFormFile? File { get; set; }
	}
}
