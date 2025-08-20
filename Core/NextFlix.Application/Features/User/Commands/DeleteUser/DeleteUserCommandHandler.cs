using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.User.Commands.DeleteUser
{
	
	public class DeleteUserCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.User, DeleteUserCommandRequest>(uow, mapper, UserMessages.DELETED, UserMessages.DELETED_ERROR, rabbitMqService, RabbitMqQueues.Users)
	{

	}
}
