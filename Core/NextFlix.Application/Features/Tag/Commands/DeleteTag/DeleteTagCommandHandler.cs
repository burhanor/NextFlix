using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Tag.Commands.DeleteTag
{


	public class DeleteTagCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Tag, DeleteTagCommandRequest>(uow, mapper, TagMessages.DELETED, TagMessages.DELETED_ERROR, rabbitMqService, RabbitMqQueues.Tags)
	{

	}
}
