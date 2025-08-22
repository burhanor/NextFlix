using NextFlix.Application.Enums;
using NextFlix.Application.Models;
using NextFlix.Domain.Enums;

namespace NextFlix.Application.Dto.MovieDtos
{
	public class MovieFilterModel:FilterModel
	{
		public string? SearchTerm { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public int? MinDuration { get; set; }
		public int? MaxDuration { get; set; }
		public Status[]? Status { get; set; }
		public List<int>? CountryIds { get; set; }
		public List<int>? TagIds { get; set; }
		public List<int>? CategoryIds { get; set; }
		public List<int>? ChannelIds { get; set; }
		public MovieOrder? OrderType { get; set; }
	}
}
