using MediatR;
using NextFlix.Application.Dto.TagDtos;
using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Tag.Queries.GetTags
{

	public class GetTagsQueryRequest :TagFilterModel, IRequest<PaginationContainer<GetTagsQueryResponse>>
	{
	}
}
