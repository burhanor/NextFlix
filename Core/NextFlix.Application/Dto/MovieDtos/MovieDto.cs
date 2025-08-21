using NextFlix.Domain.Enums;

namespace NextFlix.Application.Dto.MovieDtos
{
	public class MovieDto
	{
		public string Title { get; set; }
		public string? Description { get; set; }
		public int Duration { get; set; }
		public Status Status { get; set; }
		public string Slug { get; set; }
		public List<IdDisplayOrder>? Casts { get; set; }
		public List<IdDisplayOrder>? Countries { get; set; }
		public List<IdDisplayOrder>? Channels { get; set; }
		public List<IdDisplayOrder>? Categories { get; set; }
		public List<IdDisplayOrder>? Tags { get; set; }
		public List<MovieSourceDto>? MovieSources { get; set; }
		public List<MovieTrailerDto>? Trailers { get; set; }
	}
}
