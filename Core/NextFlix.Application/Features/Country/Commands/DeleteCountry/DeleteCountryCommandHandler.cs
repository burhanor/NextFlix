using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Country.Commands.DeleteCountry
{
	public class DeleteCountryCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Country,DeleteCountryCommandRequest>(uow, mapper, CountryMessages.DELETED,CountryMessages.DELETED_ERROR,rabbitMqService,RabbitMqQueues.Countries)
	{
		
	}
}
