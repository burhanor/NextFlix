using MediatR;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.User.Queries.UserIsExist
{
	public class UserIsExistQueryHandler(IUow uow) : BaseHandler<Domain.Entities.User>(uow),IRequestHandler<UserIsExistQueryRequest, ResponseContainer<bool>>
	{
		public async Task<ResponseContainer<bool>> Handle(UserIsExistQueryRequest request, CancellationToken cancellationToken)
		{
			bool isExists = await readRepository.ExistAsync(x => x.Id == request.Id, cancellationToken);
			ResponseContainer<bool> response = new()
			{
				Status = isExists ? ResponseStatus.Success : ResponseStatus.NotFound,
				Data = isExists
			};
			return response;
		}
	}
}
