using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Dto.MovieDtos;
using NextFlix.Application.Helpers;
using NextFlix.Domain.Entities;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Movie.Commands.CreateMovie
{
	public class CreateMovieCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService,IFileStorageService fileStorageService) : BaseHandler<Domain.Entities.Movie>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<CreateMovieCommandRequest, ResponseContainer<CreateMovieCommandResponse>>
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
			Domain.Entities.Movie movie = await ToMovieEntity(request);
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
		private async Task<Domain.Entities.Movie> ToMovieEntity(MovieDto movieDto,CancellationToken cancellationToken=default)
		{
			Domain.Entities.Movie movie = new()
			{
				Duration = movieDto.Duration,
				Title = movieDto.Title,
				Slug = movieDto.Slug,
				Status = movieDto.Status,
				UserId = userId,
				CreatedDate = DateTime.UtcNow,
				PublishDate = movieDto.Status == Domain.Enums.Status.ACCEPTED ? DateTime.UtcNow : null,
			};
			if (movieDto.Casts is not null)
			{
				IReadRepository<Domain.Entities.Cast> castReadRepository = uow.GetReadRepository<Domain.Entities.Cast>();
				var existingCasts = await castReadRepository.GetListAsync(x => movieDto.Casts.Select(c => c.Id).Contains(x.Id), select:m=>m.Id, cancellationToken: cancellationToken);
				if(existingCasts is not null)
				{
					movie.MovieCasts = existingCasts.Select(m => new MovieCast
					{
						CastId = m,
						DisplayOrder = movieDto.Casts.FirstOrDefault(c => c.Id == m)?.DisplayOrder ?? 0
					}).ToList();
				}
				
			}
			if (movieDto.Countries is not null)
			{
				IReadRepository<Domain.Entities.Country> countryReadRepository = uow.GetReadRepository<Domain.Entities.Country>();
				var existingCountries = await countryReadRepository.GetListAsync(x => movieDto.Countries.Select(c => c.Id).Contains(x.Id), select: m => m.Id, cancellationToken: cancellationToken);
				if (existingCountries is not null)
				{
					movie.MovieCountries = existingCountries.Select(m => new MovieCountry
					{
						CountryId = m,
						DisplayOrder = movieDto.Countries.FirstOrDefault(c => c.Id == m)?.DisplayOrder ?? 0
					}).ToList();
				}

			}

			if (movieDto.Channels is not null)
			{
				IReadRepository<Domain.Entities.Channel> channelReadRepository = uow.GetReadRepository<Domain.Entities.Channel>();
				var existingChannels = await channelReadRepository.GetListAsync(x => movieDto.Channels.Select(c => c.Id).Contains(x.Id), select: m => m.Id, cancellationToken: cancellationToken);
				if (existingChannels is not null)
				{
					movie.MovieChannels = existingChannels.Select(m => new MovieChannel
					{
						ChannelId = m,
						DisplayOrder = movieDto.Channels.FirstOrDefault(c => c.Id == m)?.DisplayOrder ?? 0
					}).ToList();
				}
			}

			if (movieDto.Tags is not null)
			{
				IReadRepository<Domain.Entities.Tag> tagReadRepository = uow.GetReadRepository<Domain.Entities.Tag>();
				var existingTags = await tagReadRepository.GetListAsync(x => movieDto.Tags.Select(c => c.Id).Contains(x.Id), select: m => m.Id, cancellationToken: cancellationToken);
				if (existingTags is not null)
				{
					movie.MovieTags = existingTags.Select(m => new MovieTag
					{
						TagId = m,
						DisplayOrder = movieDto.Tags.FirstOrDefault(c => c.Id == m)?.DisplayOrder ?? 0
					}).ToList();
				}
			}

			if (movieDto.Categories is not null)
			{
				IReadRepository<Domain.Entities.Category> categoryReadRepository = uow.GetReadRepository<Domain.Entities.Category>();
				var existingCategories = await categoryReadRepository.GetListAsync(x => movieDto.Categories.Select(c => c.Id).Contains(x.Id), select: m => m.Id, cancellationToken: cancellationToken);
				if (existingCategories is not null)
				{
					movie.MovieCategories = existingCategories.Select(m => new MovieCategory
					{
						CategoryId = m,
						DisplayOrder = movieDto.Categories.FirstOrDefault(c => c.Id == m)?.DisplayOrder ?? 0
					}).ToList();
				}
			}

			if(movieDto.Trailers is not null)
			{
				movie.MovieTrailers = movieDto.Trailers.Select(m => new MovieTrailer
				{
					DisplayOrder = m.DisplayOrder,
					TrailerLink = m.TrailerLink,

				}).ToList();
			}

			if(movieDto.MovieSources is not null)
			{
				IReadRepository<Domain.Entities.Source> sourceReadRepository = uow.GetReadRepository<Domain.Entities.Source>();
				var existingSources = await sourceReadRepository.GetListAsync(x => movieDto.MovieSources.Select(c => c.SourceId).Contains(x.Id), select: m => m.Id, cancellationToken: cancellationToken);
				if (existingSources is not null)
				{
					movie.MovieSources = existingSources.Select(m => new MovieSource
					{
						SourceId = m,
						DisplayOrder = movieDto.MovieSources.FirstOrDefault(c => c.SourceId == m)?.DisplayOrder ?? 0,
						Link = movieDto.MovieSources.FirstOrDefault(c => c.SourceId == m)?.Link ?? string.Empty
					}).ToList();
				}
			}
			return movie;
		}
	}
}
