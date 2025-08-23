using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Country.Commands.UpdateCountry
{
	public class UpdateCountryCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService, IFileStorageService fileStorageService) : BaseHandler<Domain.Entities.Country>(uow, mapper, rabbitMqService), IRequestHandler<UpdateCountryCommandRequest, ResponseContainer<UpdateCountryCommandResponse>>
	{
		public async Task<ResponseContainer<UpdateCountryCommandResponse>> Handle(UpdateCountryCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<UpdateCountryCommandResponse> response = await ResponseContainerHelper.Validate<UpdateCountryCommandResponse, UpdateCountryCommandValidator, UpdateCountryCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isExists = await readRepository.ExistAsync(x => x.Id == request.Id, cancellationToken);
			if (!isExists)
			{
				response.Status = ResponseStatus.NotFound;
				response.Message = CountryMessages.NOT_FOUND;
				return response;
			}


			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug && x.Id!=request.Id, cancellationToken);
			if (isSlugExists)
			{
				response.ValidationErrors =
				[
					new ValidationError
					{
						ErrorMessage = CountryMessages.SLUG_ALREADY_EXISTS,
						PropertyName = nameof(request.Slug)
					}
				];
				return response;
			}

			Domain.Entities.Country country = mapper.Map<Domain.Entities.Country>(request);
			if (request.FlagImage != null)
			{
				country.Flag = await fileStorageService.SaveFileAsync(request.FlagImage.Stream, request.FlagImage.FileName, request.FlagImage.WebRootPath, "countries", cancellationToken);
			}
			else
			{
				string? flag = await readRepository.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken, select: x => x.Flag);
				country.Flag = flag ?? string.Empty;
			}
			writeRepository.Update(country);
			await uow.SaveChangesAsync(cancellationToken);
			if (country.Id > 0)
			{
				response.Status = ResponseStatus.Updated;
				response.Message = CountryMessages.UPDATED;
				response.Data = mapper.Map<UpdateCountryCommandResponse>(country);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Countries, Abstraction.Enums.RabbitMqRoutingKeys.Updated, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = CountryMessages.UPDATED_ERROR;
			}
			return response;
		}
	}
}
