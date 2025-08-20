using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextFlix.API.Extensions;
using NextFlix.Application.Dto.ChannelDtos;
using NextFlix.Application.Features.Channel.Commands.CreateChannel;
using NextFlix.Application.Features.Channel.Commands.DeleteChannel;
using NextFlix.Application.Features.Channel.Commands.UpdateChannel;
using NextFlix.Application.Features.Channel.Queries.ChannelIsExist;
using NextFlix.Application.Features.Channel.Queries.GetChannels;
using NextFlix.Application.Features.Channel.Queries.GetChannel;
using NextFlix.Domain.Enums;
using NextFlix.Application.Features.Channel.Queries.ChannelSlugIsExist;
using NextFlix.API.Attributes;

namespace NextFlix.API.Controllers
{
	[Route("channels")]
	[ApiController]
	[RequestResponseLog]
	public class ChannelController(IMediator mediator, IWebHostEnvironment environment, IMapper mapper) : ControllerBase
	{
		[HttpGet("{id}")]
		public async Task<IActionResult> GetChannel(int id)
		{
			GetChannelQueryRequest request = new(id);
			var response = await mediator.Send(request);
			return Ok(response);
		}



		[HttpGet("exist")]
		public async Task<IActionResult> ChannelIsExist([FromQuery] int? id, [FromQuery] string? slug, [FromQuery] Status? status)
		{
			if (id.HasValue)
			{
				ChannelIsExistQueryRequest request = new(id.Value, status);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);

			}
			else
			{
				ChannelSlugExistQueryRequest request = new(slug, status);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);
			}
		}



		[HttpGet]
		public async Task<IActionResult> GetChannel([FromQuery] ChannelFilterModel model)
		{
			GetChannelsQueryRequest request = mapper.Map<GetChannelsQueryRequest>(model);
			var response = await mediator.Send(request);
			return Ok(response);
		}


		[HttpPost]
		public async Task<IActionResult> CreateChannel([FromForm] ChannelDto model, [FromForm] IFormFile? file)
		{
			CreateChannelCommandRequest request = new()
			{
				Name = model.Name,
				Slug = model.Slug,
				Status = model.Status,
				LogoImage = file.ToImageDto(environment.WebRootPath)
			};
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateChannel(int id, [FromForm] ChannelDto model, [FromForm] IFormFile? file)
		{
			UpdateChannelCommandRequest request = new()
			{
				Name = model.Name,
				Slug = model.Slug,
				Status = model.Status,
				Id = id,
				LogoImage = file.ToImageDto(environment.WebRootPath)
			};
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteChannel(int id)
		{

			DeleteChannelCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteChannel([FromBody] List<int> id)
		{
			DeleteChannelCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

	}
}
