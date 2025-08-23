using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Tag.Commands.DeleteTag
{


	public class DeleteTagCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Tag, DeleteTagCommandRequest>(uow, mapper, TagMessages.DELETED, TagMessages.DELETED_ERROR, rabbitMqService, RabbitMqQueues.Tags)
	{
		protected override async Task<List<int>> BeforeProcess(List<int> deletedIds, CancellationToken cancellationToken = default)
		{
			IReadRepository<Domain.Entities.MovieTag> movieTagReadRepository = uow.GetReadRepository<Domain.Entities.MovieTag>();
			IList<int> movieIds = await movieTagReadRepository.GetListAsync(x => deletedIds.Contains(x.TagId), x => x.MovieId, cancellationToken: cancellationToken);
			return movieIds.ToList();
		}
	}
}
