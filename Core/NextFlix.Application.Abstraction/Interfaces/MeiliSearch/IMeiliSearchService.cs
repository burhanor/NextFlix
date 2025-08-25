using NextFlix.Shared.Models;

namespace NextFlix.Application.Abstraction.Interfaces.MeiliSearch
{
	public interface IMeiliSearchService
	{
		Task<MeiliSearchResponse> SearchMoviesAsync(MovieFilterRequest request);
	}
}
