using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Cast.Commands.CreateCast
{
	

	public class CreateCastCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService, IFileStorageService fileStorageService) : BaseHandler<Domain.Entities.Cast>(uow, mapper, rabbitMqService), IRequestHandler<CreateCastCommandRequest, ResponseContainer<CreateCastCommandResponse>>
	{
		public async Task<ResponseContainer<CreateCastCommandResponse>> Handle(CreateCastCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<CreateCastCommandResponse> response = await ResponseContainerHelper.Validate<CreateCastCommandResponse, CreateCastCommandValidator, CreateCastCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug, cancellationToken);
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
				cast.Avatar = await fileStorageService.SaveFileAsync(request.AvatarImage.Stream, request.AvatarImage.FileName, request.AvatarImage.WebRootPath,"casts", cancellationToken);
			}
			await writeRepository.AddAsync(cast, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (cast.Id > 0)
			{
				response.Status = ResponseStatus.Created;
				response.Message = CastMessages.CREATED;
				response.Data = mapper.Map<CreateCastCommandResponse>(cast);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Casts, Abstraction.Enums.RabbitMqRoutingKeys.Created, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = CastMessages.CREATED_ERROR;
			}
			return response;
		}


	}
}
