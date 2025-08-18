using NextFlix.Domain.Concretes;
using NextFlix.Domain.Enums;

namespace NextFlix.Domain.Entities
{
	public class Movie:EntityBase
	{
		public string Title { get; set; }
		public string? Description { get; set; }
		public int Duration { get; set; }
		public Status Status { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? PublishDate { get; set; }
		public string? Poster { get; set; }
		public string Slug { get; set; }
		public virtual User User { get; set; }
		public virtual ICollection<MovieCast> MovieCasts { get; set; } = [];
		public virtual ICollection<MovieTag> MovieTags { get; set; } = [];
		public virtual ICollection<MovieCategory> MovieCategories { get; set; } = [];
		public virtual ICollection<MovieChannel> MovieChannels { get; set; } = [];
		public virtual ICollection<MovieSource> MovieSources { get; set; } = [];
		public virtual ICollection<MovieCountry> MovieCountries { get; set; } = [];
		public virtual ICollection<MovieTrailer> MovieTrailers { get; set; } = [];
		public virtual ICollection<MovieLike> MovieLikes { get; set; } = [];
		public virtual ICollection<MovieView> MovieViews { get; set; } = [];

	}
}
