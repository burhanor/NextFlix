using AutoMapper;
using Microsoft.AspNetCore.Http;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Dto.MovieDtos;
using NextFlix.Application.Extensions;
using NextFlix.Application.Interfaces;
using NextFlix.Domain.Entities;

namespace NextFlix.Application.Helpers
{
	public class MovieHelper(IUow uow, IHttpContextAccessor httpContextAccessor,IMapper mapper):IMovieHelper
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
				Description=movieDto.Description,
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

		public async Task<MovieResponse> FillRelations(MovieResponse movie)
		{
			movie.Casts = await GetMovieCasts(movie.Id);
			movie.Categories = await GetMovieCategories(movie.Id);
			movie.Channels = await GetMovieChannels(movie.Id);
			movie.Countries = await GetMovieCountries(movie.Id);
			movie.Sources = await GetMovieSources(movie.Id);
			movie.Tags = await GetMovieTags(movie.Id);
			movie.Trailers = await GetMovieTrailers(movie.Id);
			movie.ViewCount = await GetMovieViews(movie.Id);
			movie.Votes = await GetMovieVotes(movie.Id);
			return movie;
		}

		private async Task<List<MovieCastResponse>?> GetMovieCasts(int movieId, CancellationToken cancellationToken = default)
		{
			var relations = await uow.GetReadRepository<Domain.Entities.MovieCast>()
				.GetListAsync(m => m.MovieId == movieId, cancellationToken: cancellationToken);

			if (relations == null || relations.Count == 0)
				return null;

			var dict = relations.ToDictionary(mc => mc.CastId, mc => mc.DisplayOrder);

			var datas = await uow.GetReadRepository<Domain.Entities.Cast>()
				.GetListAsync(c => dict.Keys.Contains(c.Id), cancellationToken: cancellationToken);

			var response = datas.Select(data =>
			{
				var dto = mapper.Map<MovieCastResponse>(data);
				if (dict.TryGetValue(data.Id, out var order))
					dto.DisplayOrder = order;
				return dto;
			}).ToList();

			return response;
		}
		private async Task<List<MovieTagResponse>?> GetMovieTags(int movieId, CancellationToken cancellationToken = default)
		{
			var relations = await uow.GetReadRepository<Domain.Entities.MovieTag>()
				.GetListAsync(m => m.MovieId == movieId, cancellationToken: cancellationToken);

			if (relations == null || relations.Count == 0)
				return null;

			var dict = relations.ToDictionary(mc => mc.TagId, mc => mc.DisplayOrder);

			var datas = await uow.GetReadRepository<Domain.Entities.Tag>()
				.GetListAsync(c => dict.Keys.Contains(c.Id), cancellationToken: cancellationToken);

			var response = datas.Select(data =>
			{
				var dto = mapper.Map<MovieTagResponse>(data);
				if (dict.TryGetValue(data.Id, out var order))
					dto.DisplayOrder = order;
				return dto;
			}).ToList();

			return response;
		}
		private async Task<List<MovieChannelResponse>?> GetMovieChannels(int movieId, CancellationToken cancellationToken = default)
		{
			var relations = await uow.GetReadRepository<Domain.Entities.MovieChannel>()
				.GetListAsync(m => m.MovieId == movieId, cancellationToken: cancellationToken);

			if (relations == null || relations.Count == 0)
				return null;

			var dict = relations.ToDictionary(mc => mc.ChannelId, mc => mc.DisplayOrder);

			var datas = await uow.GetReadRepository<Domain.Entities.Channel>()
				.GetListAsync(c => dict.Keys.Contains(c.Id), cancellationToken: cancellationToken);

			var response = datas.Select(data =>
			{
				var dto = mapper.Map<MovieChannelResponse>(data);
				if (dict.TryGetValue(data.Id, out var order))
					dto.DisplayOrder = order;
				return dto;
			}).ToList();

			return response;
		}
		private async Task<List<MovieCategoryResponse>?> GetMovieCategories(int movieId, CancellationToken cancellationToken = default)
		{
			var relations = await uow.GetReadRepository<Domain.Entities.MovieCategory>()
				.GetListAsync(m => m.MovieId == movieId, cancellationToken: cancellationToken);

			if (relations == null || relations.Count == 0)
				return null;

			var dict = relations.ToDictionary(mc => mc.CategoryId, mc => mc.DisplayOrder);

			var datas = await uow.GetReadRepository<Domain.Entities.Category>()
				.GetListAsync(c => dict.Keys.Contains(c.Id), cancellationToken: cancellationToken);

			var response = datas.Select(data =>
			{
				var dto = mapper.Map<MovieCategoryResponse>(data);
				if (dict.TryGetValue(data.Id, out var order))
					dto.DisplayOrder = order;
				return dto;
			}).ToList();

			return response;
		}

		private async Task<List<MovieCountryResponse>?> GetMovieCountries(int movieId, CancellationToken cancellationToken = default)
		{
			var relations = await uow.GetReadRepository<Domain.Entities.MovieCountry>()
				.GetListAsync(m => m.MovieId == movieId, cancellationToken: cancellationToken);

			if (relations == null || relations.Count == 0)
				return null;

			var dict = relations.ToDictionary(mc => mc.CountryId, mc => mc.DisplayOrder);

			var datas = await uow.GetReadRepository<Domain.Entities.Country>()
				.GetListAsync(c => dict.Keys.Contains(c.Id), cancellationToken: cancellationToken);

			var response = datas.Select(data =>
			{
				var dto = mapper.Map<MovieCountryResponse>(data);
				if (dict.TryGetValue(data.Id, out var order))
					dto.DisplayOrder = order;
				return dto;
			}).ToList();

			return response;
		}

		private async Task<List<MovieSourceResponse>?> GetMovieSources(int movieId, CancellationToken cancellationToken = default)
		{
			var relations = await uow.GetReadRepository<Domain.Entities.MovieSource>()
				.GetListAsync(m => m.MovieId == movieId, cancellationToken: cancellationToken);

			if (relations == null || relations.Count == 0)
				return null;

			var dict = relations.ToDictionary(mc => mc.SourceId, mc => new { mc.DisplayOrder, mc.Link });

			var datas = await uow.GetReadRepository<Domain.Entities.Source>()
				.GetListAsync(c => dict.Keys.Contains(c.Id), cancellationToken: cancellationToken);

			var response = datas.Select(data =>
			{
				var dto = mapper.Map<MovieSourceResponse>(data);
				if (dict.TryGetValue(data.Id, out var info))
				{

					dto.DisplayOrder = info.DisplayOrder;
					dto.Link = info.Link;
				}

				return dto;
			}).ToList();

			return response;
		}

		private async Task<List<MovieTrailerDto>?> GetMovieTrailers(int movieId, CancellationToken cancellationToken = default)
		{
			var datas = await uow.GetReadRepository<Domain.Entities.MovieTrailer>()
				.GetListAsync(m => m.MovieId == movieId, cancellationToken: cancellationToken);
			if (datas == null || datas.Count == 0)
				return null;
			var response = mapper.Map<List<MovieTrailerDto>>(datas);
			return response;
		}

		public async Task<int> GetMovieViews(int movieId, CancellationToken cancellationToken = default)
		{
			return await uow.GetReadRepository<Domain.Entities.MovieView>()
				.CountAsync(m => m.MovieId == movieId, cancellationToken);
		}

		public async Task<List<MovieVoteResponse>?> GetMovieVotes(int movieId, CancellationToken cancellationToken = default)
		{
			IReadRepository<Domain.Entities.MovieLike> movieLikeRepository = uow.GetReadRepository<Domain.Entities.MovieLike>();

			var query = movieLikeRepository.Query().Where(m => m.MovieId == movieId).GroupBy(m => m.Vote).Select(m => new MovieVoteResponse
			{
				Vote = m.Key,
				VoteCount = m.Count()
			});
			List<MovieVoteResponse>? datas = query.ToList();
			if (datas.Count == 0)
				return null;
			await Task.CompletedTask;
			return datas;
		}

	}
}
