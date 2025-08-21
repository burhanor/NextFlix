using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Domain.Entities;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Movie.Commands.UpdateMovie
{
	public class UpdateMovieCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService, IFileStorageService fileStorageService,MovieHelper movieHelper) : BaseHandler<Domain.Entities.Movie>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<UpdateMovieCommandRequest, ResponseContainer<UpdateMovieCommandResponse>>
	{
		public async Task<ResponseContainer<UpdateMovieCommandResponse>> Handle(UpdateMovieCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<UpdateMovieCommandResponse> response = await ResponseContainerHelper.Validate<UpdateMovieCommandResponse, UpdateMovieCommandValidator, UpdateMovieCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;


			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug && x.Id!=request.Id, cancellationToken);
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
			Domain.Entities.Movie? oldMovie = await readRepository.GetAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);
			DeleteOldData(request.Id);
			if (oldMovie is null)
			{
				response.Status = ResponseStatus.NotFound;
				response.Message = MovieMessages.NOT_FOUND;
				return response;
			}
			Domain.Entities.Movie movie = await movieHelper.ToMovieEntity(request, cancellationToken);
			movie.CreatedDate = oldMovie.CreatedDate;
			movie.UserId = oldMovie.UserId;
			if (movie.Status == Domain.Enums.Status.ACCEPTED)
			{
				if(oldMovie.PublishDate.HasValue)
					movie.PublishDate =oldMovie.PublishDate.Value;
			}
			else
			{
				movie.PublishDate = null;
			}
			writeRepository.Update(movie);
			if (request.PosterImage != null)
			{
				movie.Poster = await fileStorageService.SaveFileAsync(request.PosterImage.Stream, request.PosterImage.FileName, request.PosterImage.WebRootPath, cancellationToken);
			}
			else
			{
				movie.Poster = oldMovie.Poster;
			}
			bool success = await uow.SaveChangesAsync(cancellationToken)>0;
			if (success)
			{
				response.Data = new UpdateMovieCommandResponse()
				{
					Id = request.Id
				};
				response.Status = ResponseStatus.Updated;
				response.Message = MovieMessages.UPDATED;
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Movies, Abstraction.Enums.RabbitMqRoutingKeys.Updated, response.Data, cancellationToken);

			}
			else
			{
				response.Status = ResponseStatus.Failed;
				response.Message = MovieMessages.UPDATED_ERROR;
			}
			return response;
		}



		private void DeleteOldData(int movieId)
		{
			uow.GetWriteRepository<MovieCast>().Delete(x => x.MovieId == movieId);
			uow.GetWriteRepository<MovieCategory>().Delete(x => x.MovieId == movieId);
			uow.GetWriteRepository<MovieTag>().Delete(x => x.MovieId == movieId);
			uow.GetWriteRepository<MovieChannel>().Delete(x => x.MovieId == movieId);
			uow.GetWriteRepository<MovieCountry>().Delete(x => x.MovieId == movieId);
			uow.GetWriteRepository<MovieSource>().Delete(x => x.MovieId == movieId);
			uow.GetWriteRepository<MovieTrailer>().Delete(x => x.MovieId == movieId);
		}
	}
}
