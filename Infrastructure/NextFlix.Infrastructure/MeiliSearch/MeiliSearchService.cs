using Meilisearch;
using Microsoft.Extensions.Options;
using NextFlix.Application.Abstraction.Interfaces.MeiliSearch;
using NextFlix.Domain.Entities;
using NextFlix.Shared.Models;

namespace NextFlix.Infrastructure.MeiliSearch
{
	public class MeiliSearchService:IMeiliSearchService
	{
		private readonly MeiliSearchModel _options;
		private readonly MeilisearchClient _client;
		private readonly Meilisearch.Index _index;

		public MeiliSearchService(IOptions<MeiliSearchModel> options)
		{
			_options = options.Value;
			_client = new MeilisearchClient(_options.Url, _options.MasterKey);
			_index = _client.Index(_options.IndexName);
		}

		


		public async Task<MeiliSearchResponse> SearchMoviesAsync(MovieFilterRequest request)
		{

			var filters = new List<string>();

			// ID üzerinden filtreler
			if (request.CategoryIds is { Count: > 0 })
				filters.Add($"categoryIds IN [{string.Join(",", request.CategoryIds)}]");

			if (request.TagIds is { Count: > 0 })
				filters.Add($"tagIds IN [{string.Join(",", request.TagIds)}]");

			if (request.ChannelIds is { Count: > 0 })
				filters.Add($"channelIds IN [{string.Join(",", request.ChannelIds)}]");

			if (request.CountryIds is { Count: > 0 })
				filters.Add($"countryIds IN [{string.Join(",", request.CountryIds)}]");

			if (request.MinDuration.HasValue)
				filters.Add($"duration >= {request.MinDuration.Value}");

			if (request.MaxDuration.HasValue)
				filters.Add($"duration <= {request.MaxDuration.Value}");

			// Arama koşulları
			string combinedSearchTerm = "";

			// SearchTerm doluysa, categories, channels, countries, casts, title, description içinde ara
			if (!string.IsNullOrWhiteSpace(request.SearchTerm))
			{
				combinedSearchTerm += request.SearchTerm;
			}

			// Title doluysa title alanında ara
			if (!string.IsNullOrWhiteSpace(request.Title))
			{
				if (!string.IsNullOrEmpty(combinedSearchTerm))
					combinedSearchTerm += " "; // MeiliSearch AND mantığı için boşluk
				combinedSearchTerm += $"title:{request.Title}";
			}

			// Description doluysa description alanında ara
			if (!string.IsNullOrWhiteSpace(request.Description))
			{
				if (!string.IsNullOrEmpty(combinedSearchTerm))
					combinedSearchTerm += " ";
				combinedSearchTerm += $"description:{request.Description}";
			}

			List<string> sortList = new();

			if (request.OrderType.HasValue)
			{
				switch (request.OrderType.Value)
				{
					case 0:
						sortList.Add("rating:desc");
						break;
					case 1:
						sortList.Add("viewCount:desc");
						break;
					case 2:
						sortList.Add("publishDate:desc");
						break;
					case 3:
						sortList.Add("duration:desc");
						break;
					default:
						sortList.Add("publishDate:desc");
						break;
				}
			}
			else
			{
				sortList.Add("publishDate:desc");
			}

			// MeiliSearch araması
			string filterQuery = string.Join(" AND ", filters);

			var searchResult = await _index.SearchAsync<Movie>(
				combinedSearchTerm,
				new SearchQuery
				{
					Limit = request.PageSize ?? 20,
					Offset = (request.PageNumber - 1) * (request.PageSize ?? 20),
					AttributesToRetrieve = new List<string> { "id" },
					Filter = string.IsNullOrWhiteSpace(filterQuery) ? null : filterQuery,
					Sort= sortList
				});
			var totalCount = ((Meilisearch.SearchResult<NextFlix.Domain.Entities.Movie>)searchResult).EstimatedTotalHits;
			MeiliSearchResponse response = new MeiliSearchResponse();
			response.TotalCount = totalCount;
			response.MovieIds = searchResult.Hits.Select(h => h.Id).ToList();
			return response;
		}

	}
}
