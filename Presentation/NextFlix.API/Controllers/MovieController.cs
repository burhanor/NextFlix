using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextFlix.API.Attributes;
using NextFlix.API.Extensions;
using NextFlix.Application.Dto.MovieDtos;
using NextFlix.Application.Features.Movie.Commands.CreateMovie;
using NextFlix.Application.Features.Movie.Commands.DeleteMovie;
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

	}
}
