using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Application.Interfaces;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Movie.Commands.CreateMovie
{
	public class CreateMovieCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService,IFileStorageService fileStorageService, IMovieHelper movieHelper) : BaseHandler<Domain.Entities.Movie>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<CreateMovieCommandRequest, ResponseContainer<CreateMovieCommandResponse>>
	{
		public async Task<ResponseContainer<CreateMovieCommandResponse>> Handle(CreateMovieCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<CreateMovieCommandResponse> response = await ResponseContainerHelper.Validate<CreateMovieCommandResponse, CreateMovieCommandValidator, CreateMovieCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug, cancellationToken);
			if (isSlugExists)
			{
				response.ValidationErrors =
				[
					new ValidationError
					{
						ErrorMessage = MovieMessages.SLUG_ALREADY_EXISTS,
						PropertyName = nameof(request.Slug)
					}
				];
				return response;
			}
			Domain.Entities.Movie movie = await movieHelper.ToMovieEntity(request);
			if (request.PosterImage != null)
			{
				movie.Poster = await fileStorageService.SaveFileAsync(request.PosterImage.Stream, request.PosterImage.FileName, request.PosterImage.WebRootPath, cancellationToken);
			}
			await  writeRepository.AddAsync(movie, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (movie.Id > 0)
			{
				response.Data = new CreateMovieCommandResponse { Id = movie.Id };
				response.Status = ResponseStatus.Success;
				response.Message = MovieMessages.CREATED;
				await RabbitMqService.Publish(RabbitMqQueues.Movies, RabbitMqRoutingKeys.Created, response.Data, cancellationToken);

			}
			else
			{
				response.Status = ResponseStatus.Failed;
				response.Message = MovieMessages.CREATED_ERROR;
			}
			return response;
		}

	}
}
