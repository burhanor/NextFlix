using NextFlix.Application.Dto.CountryDtos;

namespace NextFlix.Application.Dto.MovieDtos
{
	public class MovieCountryResponse:CountryResponse
	{
		public byte DisplayOrder { get; set; }
	}
}
