using NextFlix.Application.Dto.MovieDtos;

namespace NextFlix.API.Models
{
	public class MovieModel: MovieDto
	{
		public IFormFile? Poster { get; set; }
	}
}
