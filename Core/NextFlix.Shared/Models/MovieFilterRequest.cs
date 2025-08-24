namespace NextFlix.Shared.Models
{
	public class MovieFilterRequest
	{
		public string? SearchTerm { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public int? MinDuration { get; set; }
		public int? MaxDuration { get; set; }
		public List<int>? CountryIds { get; set; }
		public List<int>? TagIds { get; set; }
		public List<int>? CategoryIds { get; set; }
		public List<int>? ChannelIds { get; set; }
		public int? OrderType { get; set; }
		public int? PageSize { get; set; }
		public int? PageNumber { get; set; }
	}
}
