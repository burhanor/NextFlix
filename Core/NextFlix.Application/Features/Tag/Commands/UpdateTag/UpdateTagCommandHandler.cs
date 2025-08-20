using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Tag.Commands.UpdateTag
{
	

	public class UpdateTagCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Tag>(uow, mapper, rabbitMqService), IRequestHandler<UpdateTagCommandRequest, ResponseContainer<UpdateTagCommandResponse>>
	{
		public async Task<ResponseContainer<UpdateTagCommandResponse>> Handle(UpdateTagCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<UpdateTagCommandResponse> response = await ResponseContainerHelper.Validate<UpdateTagCommandResponse, UpdateTagCommandValidator, UpdateTagCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isExists = await readRepository.ExistAsync(x => x.Id == request.Id, cancellationToken);
			if (!isExists)
			{
				response.Status = ResponseStatus.NotFound;
				response.Message = TagMessages.NOT_FOUND;
				return response;
			}


			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug && x.Id != request.Id, cancellationToken);
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

			writeRepository.Update(tag);
			await uow.SaveChangesAsync(cancellationToken);
			if (tag.Id > 0)
			{
				response.Status = ResponseStatus.Updated;
				response.Message = TagMessages.UPDATED;
				response.Data = mapper.Map<UpdateTagCommandResponse>(tag);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Tags, Abstraction.Enums.RabbitMqRoutingKeys.Updated, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = TagMessages.UPDATED_ERROR;
			}
			return response;
		}
	}

}
