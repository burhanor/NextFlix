using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Channel.Commands.DeleteChannel
{


	public class DeleteChannelCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Channel, DeleteChannelCommandRequest>(uow, mapper, ChannelMessages.DELETED, ChannelMessages.DELETED_ERROR, rabbitMqService, RabbitMqQueues.Channels)
	{

	}
}
