using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Cast.Commands.DeleteCast
{
	

	public class DeleteCastCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Cast, DeleteCastCommandRequest>(uow, mapper, CastMessages.DELETED, CastMessages.DELETED_ERROR, rabbitMqService, RabbitMqQueues.Casts)
	{

	}
}
