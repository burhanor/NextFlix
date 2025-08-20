using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Channel.Commands.CreateChannel
{
	

	public class CreateChannelCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService, IFileStorageService fileStorageService) : BaseHandler<Domain.Entities.Channel>(uow, mapper, rabbitMqService), IRequestHandler<CreateChannelCommandRequest, ResponseContainer<CreateChannelCommandResponse>>
	{
		public async Task<ResponseContainer<CreateChannelCommandResponse>> Handle(CreateChannelCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<CreateChannelCommandResponse> response = await ResponseContainerHelper.Validate<CreateChannelCommandResponse, CreateChannelCommandValidator, CreateChannelCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug, cancellationToken);
			if (isSlugExists)
			{
				response.ValidationErrors =
				[
					new ValidationError
					{
						ErrorMessage = ChannelMessages.SLUG_ALREADY_EXISTS,
						PropertyName = nameof(request.Slug)
					}
				];
				return response;
			}



			Domain.Entities.Channel channel = mapper.Map<Domain.Entities.Channel>(request);
			if (request.LogoImage != null)
			{
				channel.Logo = await fileStorageService.SaveFileAsync(request.LogoImage.Stream, request.LogoImage.FileName, request.LogoImage.WebRootPath, cancellationToken);
			}
			await writeRepository.AddAsync(channel, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (channel.Id > 0)
			{
				response.Status = ResponseStatus.Created;
				response.Message = ChannelMessages.CREATED;
				response.Data = mapper.Map<CreateChannelCommandResponse>(channel);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Channels, Abstraction.Enums.RabbitMqRoutingKeys.Created, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = ChannelMessages.CREATED_ERROR;
			}
			return response;
		}


	}
}
