using MediatR;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Interfaces
{
	public interface IDeleteRequest:IRequest<ResponseContainer<Unit>>
	{
		public List<int> Ids { get; set; }
	}
}
