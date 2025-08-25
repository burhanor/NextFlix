namespace NextFlix.Shared.Models
{
	public class MeiliSearchResponse
	{
		public int TotalCount { get; set; }
		public List<int> MovieIds { get; set; } = [];
	}
}
