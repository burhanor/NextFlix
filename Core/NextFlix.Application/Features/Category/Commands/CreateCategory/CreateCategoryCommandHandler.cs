using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Category.Commands.CreateCategory
{


	public class CreateCategoryCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Category>(uow, mapper, rabbitMqService), IRequestHandler<CreateCategoryCommandRequest, ResponseContainer<CreateCategoryCommandResponse>>
	{
		public async Task<ResponseContainer<CreateCategoryCommandResponse>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<CreateCategoryCommandResponse> response = await ResponseContainerHelper.Validate<CreateCategoryCommandResponse, CreateCategoryCommandValidator, CreateCategoryCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug, cancellationToken);
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
			
			await writeRepository.AddAsync(category, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (category.Id > 0)
			{
				response.Status = ResponseStatus.Created;
				response.Message = CategoryMessages.CREATED;
				response.Data = mapper.Map<CreateCategoryCommandResponse>(category);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Categories, Abstraction.Enums.RabbitMqRoutingKeys.Created, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = CategoryMessages.CREATED_ERROR;
			}
			return response;
		}


	}
}
