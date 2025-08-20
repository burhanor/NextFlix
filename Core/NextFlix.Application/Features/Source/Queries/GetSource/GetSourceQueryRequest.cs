using MediatR;
using NextFlix.Shared.Interfaces;

namespace NextFlix.Application.Features.Source.Queries.GetSource
{
	public class GetSourceQueryRequest(int id) : IRequest<GetSourceQueryResponse>, IId
	{
		public int Id { get; set; } = id;
	}
}
