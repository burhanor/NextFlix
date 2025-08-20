using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Tag.Commands.CreateTag
{
	

	public class CreateTagCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Tag>(uow, mapper, rabbitMqService), IRequestHandler<CreateTagCommandRequest, ResponseContainer<CreateTagCommandResponse>>
	{
		public async Task<ResponseContainer<CreateTagCommandResponse>> Handle(CreateTagCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<CreateTagCommandResponse> response = await ResponseContainerHelper.Validate<CreateTagCommandResponse, CreateTagCommandValidator, CreateTagCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug, cancellationToken);
			if (isSlugExists)
			{
				response.ValidationErrors =
				[
					new ValidationError
					{
						ErrorMessage = TagMessages.SLUG_ALREADY_EXISTS,
						PropertyName = nameof(request.Slug)
					}
				];
				return response;
			}



			Domain.Entities.Tag tag = mapper.Map<Domain.Entities.Tag>(request);

			await writeRepository.AddAsync(tag, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (tag.Id > 0)
			{
				response.Status = ResponseStatus.Created;
				response.Message = TagMessages.CREATED;
				response.Data = mapper.Map<CreateTagCommandResponse>(tag);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Tags, Abstraction.Enums.RabbitMqRoutingKeys.Created, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = TagMessages.CREATED_ERROR;
			}
			return response;
		}


	}

}
