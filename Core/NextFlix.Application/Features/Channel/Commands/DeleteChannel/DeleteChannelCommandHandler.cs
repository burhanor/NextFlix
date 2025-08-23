using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Channel.Commands.DeleteChannel
{


	public class DeleteChannelCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Channel, DeleteChannelCommandRequest>(uow, mapper, ChannelMessages.DELETED, ChannelMessages.DELETED_ERROR, rabbitMqService, RabbitMqQueues.Channels)
	{
		protected override async Task<List<int>> BeforeProcess(List<int> deletedIds, CancellationToken cancellationToken = default)
		{
			IReadRepository<Domain.Entities.MovieChannel> movieChannelReadRepository = uow.GetReadRepository<Domain.Entities.MovieChannel>();
			IList<int> movieIds = await movieChannelReadRepository.GetListAsync(x => deletedIds.Contains(x.ChannelId), x => x.MovieId, cancellationToken: cancellationToken);
			return movieIds.ToList();
		}
	}
}
