using NextFlix.Application.Dto.CountryDtos;
using NextFlix.Application.Dto.ImageDto;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Country.Commands.UpdateCountry
{
	public class UpdateCountryCommandRequest : CountryDto, IRequestContainer<UpdateCountryCommandResponse>
	{
		public ImageDto? FlagImage { get; set; }
		public int Id { get; set; }
	}
}
