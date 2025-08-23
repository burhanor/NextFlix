using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Source.Commands.DeleteSource
{
	public class DeleteSourceCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Source, DeleteSourceCommandRequest>(uow, mapper, SourceMessages.DELETED, SourceMessages.DELETED_ERROR, rabbitMqService, RabbitMqQueues.Sources)
	{
		protected override async Task<List<int>> BeforeProcess(List<int> deletedIds, CancellationToken cancellationToken = default)
		{
			IReadRepository<Domain.Entities.MovieSource> movieSourceReadRepository = uow.GetReadRepository<Domain.Entities.MovieSource>();
			IList<int> movieIds = await movieSourceReadRepository.GetListAsync(x => deletedIds.Contains(x.SourceId), x => x.MovieId, cancellationToken: cancellationToken);
			return movieIds.ToList();
		}
	}
}
