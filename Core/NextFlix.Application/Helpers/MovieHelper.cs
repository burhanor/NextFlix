using Microsoft.AspNetCore.Http;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Dto.MovieDtos;
using NextFlix.Application.Extensions;
using NextFlix.Domain.Entities;

namespace NextFlix.Application.Helpers
{
	public class MovieHelper(IUow uow, IHttpContextAccessor httpContextAccessor)
	{
		private readonly int userId = httpContextAccessor.GetUserId();
		public async Task<Domain.Entities.Movie> ToMovieEntity(MovieDto movieDto, CancellationToken cancellationToken = default)
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
				var existingCasts = await castReadRepository.GetListAsync(x => movieDto.Casts.Select(c => c.Id).Contains(x.Id), select: m => m.Id, cancellationToken: cancellationToken);
				if (existingCasts is not null)
				{
					movie.MovieCasts = existingCasts.Distinct().Select(m => new MovieCast
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
					movie.MovieCountries = existingCountries.Distinct().Select(m => new MovieCountry
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
					movie.MovieChannels = existingChannels.Distinct().Select(m => new MovieChannel
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
					movie.MovieTags = existingTags.Distinct().Select(m => new MovieTag
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
					movie.MovieCategories = existingCategories.Distinct().Select(m => new MovieCategory
					{
						CategoryId = m,
						DisplayOrder = movieDto.Categories.FirstOrDefault(c => c.Id == m)?.DisplayOrder ?? 0
					}).ToList();
				}
			}

			if (movieDto.Trailers is not null)
			{
				movie.MovieTrailers = movieDto.Trailers.Select(m => new MovieTrailer
				{
					DisplayOrder = m.DisplayOrder,
					TrailerLink = m.TrailerLink,

				}).ToList();
			}

			if (movieDto.MovieSources is not null)
			{
				IReadRepository<Domain.Entities.Source> sourceReadRepository = uow.GetReadRepository<Domain.Entities.Source>();
				var existingSources = await sourceReadRepository.GetListAsync(x => movieDto.MovieSources.Select(c => c.SourceId).Contains(x.Id), select: m => m.Id, cancellationToken: cancellationToken);
				if (existingSources is not null)
				{
					movie.MovieSources = existingSources.Distinct().Select(m => new MovieSource
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
