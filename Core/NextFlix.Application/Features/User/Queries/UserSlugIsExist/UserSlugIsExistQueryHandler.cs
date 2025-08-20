using MediatR;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.User.Queries.UserSlugIsExist
{
	

	public class UserSlugIsExistQueryHandler(IUow uow) : BaseHandler<Domain.Entities.User>(uow), IRequestHandler<UserSlugIsExistQueryRequest, ResponseContainer<bool>>
	{
		public async Task<ResponseContainer<bool>> Handle(UserSlugIsExistQueryRequest request, CancellationToken cancellationToken)
		{
			bool isExists = await readRepository.ExistAsync(x => x.Slug == request.Slug, cancellationToken);
			ResponseContainer<bool> response = new()
			{
				Status = isExists ? ResponseStatus.Success : ResponseStatus.NotFound,
				Data = isExists
			};
			return response;
		}
	}
}
