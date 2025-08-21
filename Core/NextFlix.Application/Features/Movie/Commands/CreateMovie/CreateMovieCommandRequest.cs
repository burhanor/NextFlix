using NextFlix.Application.Dto.ImageDto;
using NextFlix.Application.Dto.MovieDtos;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Movie.Commands.CreateMovie
{
	public class CreateMovieCommandRequest:MovieDto,IRequestContainer<CreateMovieCommandResponse>
	{
		public ImageDto? PosterImage { get; set; }
	}
}
