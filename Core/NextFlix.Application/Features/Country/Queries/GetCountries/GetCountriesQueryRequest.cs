using MediatR;
using NextFlix.Application.Dto.CountryDtos;
using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Country.Queries.GetCountries
{
	public class GetCountriesQueryRequest: CountryFilterModel, IRequest<PaginationContainer<GetCountriesQueryResponse>>
	{
	}
}
