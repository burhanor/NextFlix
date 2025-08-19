using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Country.Commands.CreateCountry
{
	public class CreateCountryCommandHandler(IUow uow,IMapper mapper,IRabbitMqService rabbitMqService,IFileStorageService fileStorageService) :BaseHandler<Domain.Entities.Country>(uow,mapper,rabbitMqService), IRequestHandler<CreateCountryCommandRequest, ResponseContainer<CreateCountryCommandResponse>>
	{
		public async Task<ResponseContainer<CreateCountryCommandResponse>> Handle(CreateCountryCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<CreateCountryCommandResponse> response = await ResponseContainerHelper.Validate<CreateCountryCommandResponse, CreateCountryCommandValidator, CreateCountryCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug, cancellationToken);
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
				country.Flag = await fileStorageService.SaveFileAsync(request.FlagImage.Stream,request.FlagImage.FileName,request.FlagImage.WebRootPath,cancellationToken);
			}
			await writeRepository.AddAsync(country,cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (country.Id > 0)
			{
				response.Status = ResponseStatus.Created;
				response.Message = CountryMessages.CREATED;
				response.Data = mapper.Map<CreateCountryCommandResponse>(country);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Countries, Abstraction.Enums.RabbitMqRoutingKeys.Created, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = CountryMessages.CREATED_ERROR;
			}
			return response;
		}

	
	}
}
