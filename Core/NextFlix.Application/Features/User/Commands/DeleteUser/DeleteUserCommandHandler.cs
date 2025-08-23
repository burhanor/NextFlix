using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.User.Commands.DeleteUser
{
	
	public class DeleteUserCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.User, DeleteUserCommandRequest>(uow, mapper, UserMessages.DELETED, UserMessages.DELETED_ERROR, rabbitMqService, RabbitMqQueues.Users)
	{
		protected override async Task<List<int>> BeforeProcess(List<int> deletedIds, CancellationToken cancellationToken = default)
		{
			IReadRepository<Domain.Entities.Movie> movieReadRepository = uow.GetReadRepository<Domain.Entities.Movie>();
			IList<int> movieIds = await movieReadRepository.GetListAsync(x => deletedIds.Contains(x.UserId), x => x.Id, cancellationToken: cancellationToken);
			return movieIds.ToList();
		}
	}
}
