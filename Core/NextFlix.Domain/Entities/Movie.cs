using NextFlix.Domain.Concretes;
using NextFlix.Domain.Enums;

namespace NextFlix.Domain.Entities
{
	public class Movie:EntityBase
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public int Duration { get; set; }
		public Status Status { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? PublishDate { get; set; }
		public string? Poster { get; set; }
	}
}
