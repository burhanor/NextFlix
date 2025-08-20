using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Source.Commands.CreateSource
{
	

	public class CreateSourceCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Source>(uow, mapper, rabbitMqService), IRequestHandler<CreateSourceCommandRequest, ResponseContainer<CreateSourceCommandResponse>>
	{
		public async Task<ResponseContainer<CreateSourceCommandResponse>> Handle(CreateSourceCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<CreateSourceCommandResponse> response = await ResponseContainerHelper.Validate<CreateSourceCommandResponse, CreateSourceCommandValidator, CreateSourceCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isTitleExists = await readRepository.ExistAsync(x => x.Title == request.Title, cancellationToken);
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

			await writeRepository.AddAsync(source, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (source.Id > 0)
			{
				response.Status = ResponseStatus.Created;
				response.Message = SourceMessages.CREATED;
				response.Data = mapper.Map<CreateSourceCommandResponse>(source);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Sources, Abstraction.Enums.RabbitMqRoutingKeys.Created, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = SourceMessages.CREATED_ERROR;
			}
			return response;
		}


	}

}
