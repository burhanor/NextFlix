using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Category.Commands.DeleteCategory
{


	public class DeleteCategoryCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Category, DeleteCategoryCommandRequest>(uow, mapper, CategoryMessages.DELETED, CategoryMessages.DELETED_ERROR, rabbitMqService, RabbitMqQueues.Categories)
	{

	}
}
