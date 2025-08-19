using MediatR;
using NextFlix.Domain.Enums;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Interfaces
{
	public interface ISlugIsExistRequest:IRequest<ResponseContainer<bool>>
	{
		public string Slug { get; set; }
		public Status? Status { get; set; }
	}
}
