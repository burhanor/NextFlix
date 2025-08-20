using MediatR;
using NextFlix.Shared.Interfaces;

namespace NextFlix.Application.Features.Tag.Queries.GetTag
{
	public class GetTagQueryRequest(int id) : IRequest<GetTagQueryResponse>, IId
	{
		public int Id { get; set; } = id;
	}
}
