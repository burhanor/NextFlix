using AutoMapper;
using NextFlix.Application.Dto.MovieDtos;
using NextFlix.Application.Features.Movie.Commands.CreateMovie;
using NextFlix.Application.Features.Movie.Commands.UpdateMovie;
using NextFlix.Application.Features.Movie.Queries.GetMovie;
using NextFlix.Application.Features.Movie.Queries.GetMovieBySlug;
using NextFlix.Application.Features.Movie.Queries.GetMovies;
using NextFlix.Shared.Models;

namespace NextFlix.Application.Mappings
{
	internal class MovieMappingProfile : Profile
	{
		public MovieMappingProfile()
		{
			CreateMap<MovieDto, CreateMovieCommandRequest>().ReverseMap();
			CreateMap<MovieDto,Domain.Entities.Movie>();


			CreateMap<MovieDto, UpdateMovieCommandRequest>().ReverseMap();

			CreateMap<Domain.Entities.Movie, GetMovieQueryResponse>();
			CreateMap<Domain.Entities.Movie, GetMovieBySlugQueryResponse>();
			CreateMap<Domain.Entities.Movie, MovieResponse>();
			CreateMap<MovieResponse, GetMovieQueryResponse>().ReverseMap();
			CreateMap<MovieResponse, GetMovieBySlugQueryResponse>().ReverseMap();
			CreateMap<Domain.Entities.Cast, MovieCastResponse>();
			CreateMap<Domain.Entities.Tag, MovieTagResponse>();
			CreateMap<Domain.Entities.Channel, MovieChannelResponse>();
			CreateMap<Domain.Entities.Category, MovieCategoryResponse>();
			CreateMap<Domain.Entities.Country, MovieCountryResponse>();
			CreateMap<Domain.Entities.Source, MovieSourceResponse>();
			CreateMap<Domain.Entities.MovieTrailer, MovieTrailerDto>();

			CreateMap<GetMoviesQueryRequest, MovieFilterRequest>().ReverseMap();
		}
	}
}
