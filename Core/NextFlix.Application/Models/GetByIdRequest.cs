using MediatR;

namespace NextFlix.Application.Models
{
	public class GetByIdRequest<TResponse>:IRequest<TResponse?>
	{
	}
}
