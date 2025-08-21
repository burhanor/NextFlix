using AutoMapper;
using NextFlix.Application.Dto.MovieDtos;
using NextFlix.Application.Features.Movie.Commands.CreateMovie;

namespace NextFlix.Application.Mappings
{
	internal class MovieMappingProfile : Profile
	{
		public MovieMappingProfile()
		{
			CreateMap<MovieDto, CreateMovieCommandRequest>().ReverseMap();
			CreateMap<MovieDto,Domain.Entities.Movie>();
		}
	}
}
