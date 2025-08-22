using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextFlix.API.Attributes;
using NextFlix.API.Extensions;
using NextFlix.API.Models;
using NextFlix.Application.Dto.CastDtos;
using NextFlix.Application.Features.Cast.Commands.CreateCast;
using NextFlix.Application.Features.Cast.Commands.DeleteCast;
using NextFlix.Application.Features.Cast.Commands.UpdateCast;
using NextFlix.Application.Features.Cast.Queries.CastIsExist;
using NextFlix.Application.Features.Cast.Queries.CastSlugExist;
using NextFlix.Application.Features.Cast.Queries.GetCast;
using NextFlix.Application.Features.Cast.Queries.GetCasts;
using NextFlix.Domain.Enums;

namespace NextFlix.API.Controllers
{
	[Route("casts")]
	[ApiController]
	[RequestResponseLog]
	public class CastController(IMediator mediator, IWebHostEnvironment environment, IMapper mapper) : ControllerBase
	{


		[HttpGet("{id}")]
		public async Task<IActionResult> GetCast(int id)
		{
			GetCastQueryRequest request = new(id);
			var response = await mediator.Send(request);
			return Ok(response);
		}



		[HttpGet("exist")]
		public async Task<IActionResult> CastIsExist([FromQuery] int? id, [FromQuery] string? slug, [FromQuery] Status? status)
		{
			if (id.HasValue)
			{
				CastIsExistQueryRequest request = new(id.Value, status);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);

			}
			else
			{
				CastSlugExistQueryRequest request = new(slug, status);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);
			}
		}



		[HttpGet]
		public async Task<IActionResult> GetCast([FromQuery] CastFilterModel model)
		{
			GetCastsQueryRequest request = mapper.Map<GetCastsQueryRequest>(model);
			var response = await mediator.Send(request);
			return Ok(response);
		}


		[HttpPost]
		public async Task<IActionResult> CreateCast([FromForm] CastModel model)
		{
			CreateCastCommandRequest request = new()
			{
				Name = model.Name,
				Slug = model.Slug,
				Status = model.Status,
				Biography=model.Biography,
				BirthDate=model.BirthDate,
				CastType=model.CastType,
				Gender=model.Gender,
				CountryId=model.CountryId,
				AvatarImage = model.File.ToImageDto(environment.WebRootPath)
			};
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCast(int id, [FromForm] CastModel model)
		{
			UpdateCastCommandRequest request = new()
			{
				Name = model.Name,
				Slug = model.Slug,
				Status = model.Status,
				Biography = model.Biography,
				BirthDate = model.BirthDate,
				CastType = model.CastType,
				Gender = model.Gender,
				CountryId = model.CountryId,
				Id = id,
				AvatarImage = model.File.ToImageDto(environment.WebRootPath)
			};
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCast(int id)
		{

			DeleteCastCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCast([FromBody] List<int> id)
		{
			DeleteCastCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

	}
}
