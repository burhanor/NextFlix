using MediatR;
using NextFlix.Application.Interfaces;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Models
{
	public class DeleteRequest:IDeleteRequest, IRequest<ResponseContainer<Unit>>
	{
		public DeleteRequest()
		{

		}
		public DeleteRequest(int id)
		{
			Ids = [id];
		}
		public DeleteRequest(List<int> ids)
		{
			Ids = ids;
		}
		public List<int> Ids { get; set; } = [];
	}
}
