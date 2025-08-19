using MediatR;
using NextFlix.Shared.Interfaces;

namespace NextFlix.Application.Features.Country.Queries.GetCountry
{
	public class GetCountryQueryRequest(int id) : IRequest<GetCountryQueryResponse>,IId
	{
		public int Id { get; set; } = id;
	}
}
