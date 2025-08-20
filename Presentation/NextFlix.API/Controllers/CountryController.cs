using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextFlix.API.Extensions;
using NextFlix.Application.Dto.CountryDtos;
using NextFlix.Application.Features.Country.Commands.CreateCountry;
using NextFlix.Application.Features.Country.Commands.DeleteCountry;
using NextFlix.Application.Features.Country.Commands.UpdateCountry;
using NextFlix.Application.Features.Country.Queries.CountryIsExist;
using NextFlix.Application.Features.Country.Queries.GetCountries;
using NextFlix.Application.Features.Country.Queries.GetCountry;
using NextFlix.Application.Features.Country.Queries.SlugIsExist;
using NextFlix.Domain.Enums;

namespace NextFlix.API.Controllers
{
	[Route("countries")]
	[ApiController]
	public class CountryController(IMediator mediator, IWebHostEnvironment environment,IMapper mapper) : ControllerBase
	{


		[HttpGet("{id}")]
		public async Task<IActionResult> GetCountry(int id)
		{
			GetCountryQueryRequest request = new(id);
			var response = await mediator.Send(request);
			return Ok(response);
		}
	


		[HttpGet("exist")]
		public async Task<IActionResult> CountryIsExist( [FromQuery] int? id, [FromQuery] string? slug, [FromQuery] Status? status)
		{
			if (id.HasValue)
			{
				CountryIsExistQueryRequest request = new(id.Value, status);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);

			}
			else
			{
				SlugIsExistQueryRequest request = new(slug, status);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);
			}
		}



		[HttpGet]
		public async Task<IActionResult> GetCountry([FromQuery]CountryFilterModel model)
		{
			GetCountriesQueryRequest request  = mapper.Map<GetCountriesQueryRequest>(model);
			var response = await mediator.Send(request);
			return Ok(response);
		}


		[HttpPost]
		public async Task<IActionResult> CreateCountry([FromForm] CountryDto model, [FromForm] IFormFile? file) 
		{
			CreateCountryCommandRequest request = new()
			{
				Name = model.Name,
				Slug = model.Slug,
				Status = model.Status,
				FlagImage = file.ToImageDto(environment.WebRootPath)
			};
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCountry(int id,[FromForm] CountryDto model, [FromForm] IFormFile? file)
		{
			UpdateCountryCommandRequest request = new()
			{
				Name = model.Name,
				Slug = model.Slug,
				Status = model.Status,
				Id = id,
				FlagImage = file.ToImageDto(environment.WebRootPath)
			};
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCountry(int id) {

			DeleteCountryCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCountry([FromBody] List<int> id)
		{
			DeleteCountryCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

	}
}
