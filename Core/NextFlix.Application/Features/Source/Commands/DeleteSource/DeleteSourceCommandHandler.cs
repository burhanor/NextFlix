using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Source.Commands.DeleteSource
{
	public class DeleteSourceCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Source, DeleteSourceCommandRequest>(uow, mapper, SourceMessages.DELETED, SourceMessages.DELETED_ERROR, rabbitMqService, RabbitMqQueues.Sources)
	{

	}
}
