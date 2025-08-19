using AutoMapper;
using NextFlix.Application.Dto.CountryDtos;
using NextFlix.Application.Features.Country.Commands.CreateCountry;
using NextFlix.Application.Features.Country.Commands.UpdateCountry;
using NextFlix.Application.Features.Country.Queries.GetCountries;
using NextFlix.Application.Features.Country.Queries.GetCountry;

namespace NextFlix.Application.Mappings
{
	internal class CountryMappingProfile:Profile
	{
		public CountryMappingProfile()
		{
			CreateMap<CountryDto, CreateCountryCommandRequest>();
			CreateMap<CreateCountryCommandRequest, Domain.Entities.Country>();
			CreateMap<Domain.Entities.Country, CreateCountryCommandResponse>();

			CreateMap<CountryDto, UpdateCountryCommandRequest>();
			CreateMap<UpdateCountryCommandRequest, Domain.Entities.Country>();
			CreateMap<Domain.Entities.Country, UpdateCountryCommandResponse>();


			CreateMap<Domain.Entities.Country, GetCountryQueryResponse>();

			CreateMap<CountryFilterModel, GetCountriesQueryRequest>();
			CreateMap<Domain.Entities.Country, GetCountriesQueryResponse>();
			CreateMap<Domain.Entities.Country, GetCountryQueryResponse>();

		}
	}
}
