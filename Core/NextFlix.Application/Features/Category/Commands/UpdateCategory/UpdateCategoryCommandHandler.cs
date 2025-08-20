using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Category.Commands.UpdateCategory
{


	public class UpdateCategoryCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Category>(uow, mapper, rabbitMqService), IRequestHandler<UpdateCategoryCommandRequest, ResponseContainer<UpdateCategoryCommandResponse>>
	{
		public async Task<ResponseContainer<UpdateCategoryCommandResponse>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<UpdateCategoryCommandResponse> response = await ResponseContainerHelper.Validate<UpdateCategoryCommandResponse, UpdateCategoryCommandValidator, UpdateCategoryCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isExists = await readRepository.ExistAsync(x => x.Id == request.Id, cancellationToken);
			if (!isExists)
			{
				response.Status = ResponseStatus.NotFound;
				response.Message = CategoryMessages.NOT_FOUND;
				return response;
			}


			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug && x.Id != request.Id, cancellationToken);
			if (isSlugExists)
			{
				response.ValidationErrors =
				[
					new ValidationError
					{
						ErrorMessage = CategoryMessages.SLUG_ALREADY_EXISTS,
						PropertyName = nameof(request.Slug)
					}
				];
				return response;
			}

			Domain.Entities.Category category = mapper.Map<Domain.Entities.Category>(request);
			
			writeRepository.Update(category);
			await uow.SaveChangesAsync(cancellationToken);
			if (category.Id > 0)
			{
				response.Status = ResponseStatus.Updated;
				response.Message = CategoryMessages.UPDATED;
				response.Data = mapper.Map<UpdateCategoryCommandResponse>(category);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Categories, Abstraction.Enums.RabbitMqRoutingKeys.Updated, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = CategoryMessages.UPDATED_ERROR;
			}
			return response;
		}
	}


}
