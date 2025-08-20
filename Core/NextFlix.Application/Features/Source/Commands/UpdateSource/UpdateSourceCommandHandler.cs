using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Source.Commands.UpdateSource
{
	internal class UpdateSourceCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Source>(uow, mapper, rabbitMqService), IRequestHandler<UpdateSourceCommandRequest, ResponseContainer<UpdateSourceCommandResponse>>
	{
		public async Task<ResponseContainer<UpdateSourceCommandResponse>> Handle(UpdateSourceCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<UpdateSourceCommandResponse> response = await ResponseContainerHelper.Validate<UpdateSourceCommandResponse, UpdateSourceCommandValidator, UpdateSourceCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isExists = await readRepository.ExistAsync(x => x.Id == request.Id, cancellationToken);
			if (!isExists)
			{
				response.Status = ResponseStatus.NotFound;
				response.Message = SourceMessages.NOT_FOUND;
				return response;
			}


			bool isTitleExists = await readRepository.ExistAsync(x => x.Title == request.Title && x.Id != request.Id, cancellationToken);
			if (isTitleExists)
			{
				response.ValidationErrors =
				[
					new ValidationError
					{
						ErrorMessage = SourceMessages.TITLE_ALREADY_EXISTS,
						PropertyName = nameof(request.Title)
					}
				];
				return response;
			}
			Domain.Entities.Source source = mapper.Map<Domain.Entities.Source>(request);
			writeRepository.Update(source);
			await uow.SaveChangesAsync(cancellationToken);
			if (source.Id > 0)
			{
				response.Status = ResponseStatus.Updated;
				response.Message = SourceMessages.UPDATED;
				response.Data = mapper.Map<UpdateSourceCommandResponse>(source);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Sources, Abstraction.Enums.RabbitMqRoutingKeys.Updated, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = SourceMessages.UPDATED_ERROR;
			}
			return response;
		}

	}


}
