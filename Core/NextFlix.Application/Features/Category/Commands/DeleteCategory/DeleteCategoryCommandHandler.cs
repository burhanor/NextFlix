using AutoMapper;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Category.Commands.DeleteCategory
{


	public class DeleteCategoryCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Category, DeleteCategoryCommandRequest>(uow, mapper, CategoryMessages.DELETED, CategoryMessages.DELETED_ERROR, rabbitMqService, RabbitMqQueues.Categories)
	{
		protected override async Task<List<int>> BeforeProcess(List<int> deletedIds, CancellationToken cancellationToken = default)
		{
			IReadRepository<Domain.Entities.MovieCategory> movieCategoryReadRepository = uow.GetReadRepository<Domain.Entities.MovieCategory>();
			IList<int> movieIds = await movieCategoryReadRepository.GetListAsync(x => deletedIds.Contains(x.CategoryId), x => x.MovieId, cancellationToken: cancellationToken);
			return movieIds.ToList();
		}
	}
}
