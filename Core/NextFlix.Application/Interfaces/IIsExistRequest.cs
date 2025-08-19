using MediatR;
using NextFlix.Domain.Enums;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Interfaces
{
	public interface IIsExistRequest:IRequest<ResponseContainer<bool>>
	{
		public int Id { get; set; }
		public Status? Status { get; set; }
	}
}
