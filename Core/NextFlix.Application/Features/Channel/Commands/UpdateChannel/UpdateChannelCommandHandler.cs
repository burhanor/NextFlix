using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Channel.Commands.UpdateChannel
{
	

	public class UpdateChannelCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService, IFileStorageService fileStorageService) : BaseHandler<Domain.Entities.Channel>(uow, mapper, rabbitMqService), IRequestHandler<UpdateChannelCommandRequest, ResponseContainer<UpdateChannelCommandResponse>>
	{
		public async Task<ResponseContainer<UpdateChannelCommandResponse>> Handle(UpdateChannelCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<UpdateChannelCommandResponse> response = await ResponseContainerHelper.Validate<UpdateChannelCommandResponse, UpdateChannelCommandValidator, UpdateChannelCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isExists = await readRepository.ExistAsync(x => x.Id == request.Id, cancellationToken);
			if (!isExists)
			{
				response.Status = ResponseStatus.NotFound;
				response.Message = ChannelMessages.NOT_FOUND;
				return response;
			}


			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug && x.Id != request.Id, cancellationToken);
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
			else
			{
				string? flag = await readRepository.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken, select: x => x.Logo);
				channel.Logo = flag ?? string.Empty;
			}
			writeRepository.Update(channel);
			await uow.SaveChangesAsync(cancellationToken);
			if (channel.Id > 0)
			{
				response.Status = ResponseStatus.Updated;
				response.Message = ChannelMessages.UPDATED;
				response.Data = mapper.Map<UpdateChannelCommandResponse>(channel);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Channels, Abstraction.Enums.RabbitMqRoutingKeys.Updated, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = ChannelMessages.UPDATED_ERROR;
			}
			return response;
		}
	}
}
