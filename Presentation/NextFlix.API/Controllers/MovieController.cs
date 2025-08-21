using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextFlix.API.Attributes;
using NextFlix.API.Extensions;
using NextFlix.Application.Dto.MovieDtos;
using NextFlix.Application.Features.Movie.Commands.CreateMovie;
using NextFlix.Application.Features.Movie.Commands.DeleteMovie;
using NextFlix.Application.Features.Movie.Commands.UpdateMovie;
using NextFlix.Application.Features.Movie.Commands.VoteMovie;
using NextFlix.Application.Features.Movie.Commands.WatchMovie;
using NextFlix.Application.Features.Movie.Queries.GetMovie;
using NextFlix.Application.Features.Movie.Queries.MovieIsExist;
using NextFlix.Application.Features.Movie.Queries.MovieSlugIsExist;
using NextFlix.Domain.Enums;

namespace NextFlix.API.Controllers
{
	[Route("movies")]
	[ApiController]
	[RequestResponseLog]
	public class MovieController(IMediator mediator,IMapper mapper,IWebHostEnvironment environment) : ControllerBase
	{
		[HttpGet("{id}")]
		public async Task<IActionResult> GetMovie(int id)
		{
			GetMovieQueryRequest request = new(id);
			var response = await mediator.Send(request);
			return Ok(response);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateMovie([FromForm] MovieDto model, [FromForm] IFormFile? Poster)
		{
			model.MapJsonListsFromForm(HttpContext.Request.Form);
			CreateMovieCommandRequest request = mapper.Map<CreateMovieCommandRequest>(model);

			request.PosterImage = Poster.ToImageDto(environment.WebRootPath);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateMovie(int id, [FromForm] MovieDto model, [FromForm] IFormFile? Poster)
		{
			model.MapJsonListsFromForm(HttpContext.Request.Form);
			UpdateMovieCommandRequest request = mapper.Map<UpdateMovieCommandRequest>(model);
			request.Id = id;
			request.PosterImage = Poster.ToImageDto(environment.WebRootPath);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}
		

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMovie(int id)
		{
			DeleteMovieCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteMovie([FromBody] List<int> id)
		{
			DeleteMovieCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}


		[HttpGet("exist")]
		public async Task<IActionResult> MovieIsExist([FromQuery] int? id, [FromQuery] string? slug, [FromQuery] Status? status)
		{
			if (id.HasValue)
			{
				MovieIsExistQueryRequest request = new(id.Value, status);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);

			}
			else
			{
				MovieSlugIsExistQueryRequest request = new(slug, status);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);
			}
		}

		[HttpPost("{id}/votes")]
		public async Task<IActionResult> VoteMovie(int id, VoteType voteType)
		{
			VoteMovieCommandRequest request = new(id, voteType);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpPost("{id}/views")]
		public async Task<IActionResult> ViewMovie(int id)
		{
			WatchMovieCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

	}
}
