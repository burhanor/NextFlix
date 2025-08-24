using NextFlix.Shared.Models;

namespace NextFlix.Application.Abstraction.Interfaces.MeiliSearch
{
	public interface IMeiliSearchService
	{
		Task<List<int>> SearchMoviesAsync(MovieFilterRequest request);
	}
}
