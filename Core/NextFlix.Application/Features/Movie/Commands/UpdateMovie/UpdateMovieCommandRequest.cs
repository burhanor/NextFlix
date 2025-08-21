using NextFlix.Application.Dto.ImageDto;
using NextFlix.Application.Dto.MovieDtos;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Movie.Commands.UpdateMovie
{
	public class UpdateMovieCommandRequest : MovieDto, IRequestContainer<UpdateMovieCommandResponse>
	{
		public int Id { get; set; }
		public ImageDto? PosterImage { get; set; }
	}
}
