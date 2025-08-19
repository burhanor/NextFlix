using MediatR;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Interfaces;
using NextFlix.Domain.Interfaces;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Bases
{
	public class IsExistHandler<T, TRequest>(IUow uow) : BaseHandler<T>(uow), IRequestHandler<TRequest, ResponseContainer<bool>>
		where T : class, IEntityBase,IStatus, new()
		where TRequest: IIsExistRequest
	{
		public async Task<ResponseContainer<bool>> Handle(TRequest request, CancellationToken cancellationToken)
		{
			bool isExists = request.Status.HasValue ? await readRepository.ExistAsync(x => x.Id == request.Id && x.Status == request.Status, cancellationToken)
				: await readRepository.ExistAsync(x => x.Id == request.Id, cancellationToken);
			ResponseContainer<bool> response = new()
			{
				Status = isExists ? ResponseStatus.Success : ResponseStatus.NotFound,
				Data = isExists
			};
			return response;
		}
	}
}
