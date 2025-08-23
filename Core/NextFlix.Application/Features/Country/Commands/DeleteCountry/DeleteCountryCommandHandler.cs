using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Country.Commands.DeleteCountry
{
	public class DeleteCountryCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Country,DeleteCountryCommandRequest>(uow, mapper, CountryMessages.DELETED,CountryMessages.DELETED_ERROR,rabbitMqService,RabbitMqQueues.Countries)
	{
		protected override async Task<List<int>> BeforeProcess(List<int> deletedIds, CancellationToken cancellationToken = default)
		{
			IReadRepository<Domain.Entities.MovieCountry> movieCountryReadRepository = uow.GetReadRepository<Domain.Entities.MovieCountry>();
			IList<int> movieIds = await movieCountryReadRepository.GetListAsync(x => deletedIds.Contains(x.CountryId), x => x.MovieId, cancellationToken: cancellationToken);
			return movieIds.ToList();
		}
	}
}
