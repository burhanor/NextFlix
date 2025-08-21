using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Dto.MovieDtos;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Movie.Queries.GetMovie
{
	public class GetMovieQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IMovieHelper movieHelper) : BaseHandler<Domain.Entities.Movie>(uow, httpContextAccessor, mapper), IRequestHandler<GetMovieQueryRequest, GetMovieQueryResponse?>
	{
		public async Task<GetMovieQueryResponse?> Handle(GetMovieQueryRequest request, CancellationToken cancellationToken)
		{
			Domain.Entities.Movie? movie = await readRepository.GetAsync(request.MovieId,cancellationToken:cancellationToken);
			if (movie is null)
				return null;
			MovieResponse movieResponse = mapper.Map<MovieResponse>(movie);
			movieResponse = await movieHelper.FillRelations(movieResponse);
			GetMovieQueryResponse response = mapper.Map<GetMovieQueryResponse>(movieResponse);
			return response; 
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

		private async Task<List<MovieTrailerDto>?> GetMovieTrailers(int movieId,CancellationToken cancellationToken=default)
		{
			var datas = await uow.GetReadRepository<Domain.Entities.MovieTrailer>()
				.GetListAsync(m => m.MovieId == movieId, cancellationToken: cancellationToken);
			if (datas == null || datas.Count == 0)
				return null;
			var response = mapper.Map<List<MovieTrailerDto>>(datas);
			return response;
		}

		private async Task<int> GetMovieViews(int movieId, CancellationToken cancellationToken = default)
		{
			return await uow.GetReadRepository<Domain.Entities.MovieView>()
				.CountAsync(m => m.MovieId == movieId, cancellationToken);
		}

		private   List<MovieVoteResponse>? GetMovieVotes(int movieId,CancellationToken cancellationToken = default)
		{
			IReadRepository<Domain.Entities.MovieLike> movieLikeRepository = uow.GetReadRepository<Domain.Entities.MovieLike>();

			var query = movieLikeRepository.Query().Where(m => m.MovieId == movieId).GroupBy(m=>m.Vote).Select(m=>new MovieVoteResponse
			{
				Vote=m.Key,
				VoteCount =m.Count()
			});
			List<MovieVoteResponse>? datas = query.ToList();
			if (datas == null || datas.Count == 0)
				return null;

			return datas;
		}
	}
}
