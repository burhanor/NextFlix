using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Cast.Commands.DeleteCast
{
	

	public class DeleteCastCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Cast, DeleteCastCommandRequest>(uow, mapper, CastMessages.DELETED, CastMessages.DELETED_ERROR, rabbitMqService, RabbitMqQueues.Casts)
	{
		protected override async Task<List<int>> BeforeProcess(List<int> deletedIds, CancellationToken cancellationToken = default)
		{
			IReadRepository<Domain.Entities.MovieCast> movieCastReadRepository = uow.GetReadRepository<Domain.Entities.MovieCast>();
			IList<int> movieIds = await movieCastReadRepository.GetListAsync(x => deletedIds.Contains(x.CastId), x => x.MovieId, cancellationToken: cancellationToken);
			return movieIds.ToList();
		}
	}
}
