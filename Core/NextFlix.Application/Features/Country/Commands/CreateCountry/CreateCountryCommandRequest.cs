using NextFlix.Application.Dto.CountryDtos;
using NextFlix.Application.Dto.ImageDto;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Country.Commands.CreateCountry
{
	public class CreateCountryCommandRequest:CountryDto, IRequestContainer<CreateCountryCommandResponse>
	{
		public ImageDto? FlagImage { get; set; }
	}
}
