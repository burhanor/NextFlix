using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Cast.Commands.UpdateCast
{
	


	public class UpdateCastCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService, IFileStorageService fileStorageService) : BaseHandler<Domain.Entities.Cast>(uow, mapper, rabbitMqService), IRequestHandler<UpdateCastCommandRequest, ResponseContainer<UpdateCastCommandResponse>>
	{
		public async Task<ResponseContainer<UpdateCastCommandResponse>> Handle(UpdateCastCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<UpdateCastCommandResponse> response = await ResponseContainerHelper.Validate<UpdateCastCommandResponse, UpdateCastCommandValidator, UpdateCastCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isExists = await readRepository.ExistAsync(x => x.Id == request.Id, cancellationToken);
			if (!isExists)
			{
				response.Status = ResponseStatus.NotFound;
				response.Message = CastMessages.NOT_FOUND;
				return response;
			}


			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug && x.Id != request.Id, cancellationToken);
			if (isSlugExists)
			{
				response.ValidationErrors =
				[
					new ValidationError
					{
						ErrorMessage = CastMessages.SLUG_ALREADY_EXISTS,
						PropertyName = nameof(request.Slug)
					}
				];
				return response;
			}

			bool isCountryExist = await uow.GetReadRepository<Domain.Entities.Country>().ExistAsync(x => x.Id == request.CountryId, cancellationToken);
			if (!isCountryExist)
			{
				response.ValidationErrors =
				[
					new ValidationError
					{
						ErrorMessage = CastMessages.COUNTRY_NOT_FOUND,
						PropertyName = nameof(request.CountryId)
					}
				];
				return response;
			}

			Domain.Entities.Cast cast = mapper.Map<Domain.Entities.Cast>(request);
			if (request.AvatarImage != null)
			{
				cast.Avatar = await fileStorageService.SaveFileAsync(request.AvatarImage.Stream, request.AvatarImage.FileName, request.AvatarImage.WebRootPath, cancellationToken);
			}
			else
			{
				string? avatar = await readRepository.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken, select: x => x.Avatar);
				cast.Avatar = avatar ?? string.Empty;
			}
			writeRepository.Update(cast);
			await uow.SaveChangesAsync(cancellationToken);
			if (cast.Id > 0)
			{
				response.Status = ResponseStatus.Updated;
				response.Message = CastMessages.UPDATED;
				response.Data = mapper.Map<UpdateCastCommandResponse>(cast);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Casts, Abstraction.Enums.RabbitMqRoutingKeys.Updated, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = CastMessages.UPDATED_ERROR;
			}
			return response;
		}
	}
}
