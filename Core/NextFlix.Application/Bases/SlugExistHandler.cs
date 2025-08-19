using MediatR;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Interfaces;
using NextFlix.Domain.Interfaces;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Bases
{


	public class SlugExistHandler<T, TRequest>(IUow uow) : BaseHandler<T>(uow), IRequestHandler<TRequest, ResponseContainer<bool>>
		where T : class, IEntityBase, IStatus,ISlug, new()
		where TRequest : ISlugIsExistRequest
	{
		public async Task<ResponseContainer<bool>> Handle(TRequest request, CancellationToken cancellationToken)
		{
			bool isExists = request.Status.HasValue ? await readRepository.ExistAsync(x => x.Slug == request.Slug && x.Status == request.Status, cancellationToken)
				: await readRepository.ExistAsync(x => x.Slug == request.Slug, cancellationToken);
			ResponseContainer<bool> response = new()
			{
				Status = isExists ? ResponseStatus.Success : ResponseStatus.NotFound,
				Data = isExists
			};
			return response;
		}
	}
}
