using MediatR;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Interfaces
{
	public interface IRequestContainer<T>:IRequest<ResponseContainer<T>>
	{
	}
}
