using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Movie.Commands.DeleteMovie
{
	
	public class DeleteMovieCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Movie, DeleteMovieCommandRequest>(uow, mapper, MovieMessages.DELETED, MovieMessages.DELETED_ERROR, rabbitMqService, RabbitMqQueues.Movies)
	{

	}
}
