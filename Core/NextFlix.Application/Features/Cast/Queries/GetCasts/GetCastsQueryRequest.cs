using MediatR;
using NextFlix.Application.Dto.CastDtos;
using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Cast.Queries.GetCasts
{
	public class GetCastsQueryRequest : CastFilterModel, IRequest<PaginationContainer<GetCastsQueryResponse>>
	{
	}
}
