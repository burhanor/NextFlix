using MediatR;
using NextFlix.Application.Dto.SourceDtos;
using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Source.Queries.GetSources
{
	public class GetSourcesQueryRequest : SourceFilterModel, IRequest<PaginationContainer<GetSourcesQueryResponse>>
	{
	}
}
