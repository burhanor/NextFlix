using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextFlix.API.Extensions;
using NextFlix.Application.Dto.TagDtos;
using NextFlix.Application.Features.Tag.Commands.CreateTag;
using NextFlix.Application.Features.Tag.Commands.DeleteTag;
using NextFlix.Application.Features.Tag.Commands.UpdateTag;
using NextFlix.Application.Features.Tag.Queries.GetTag;
using NextFlix.Application.Features.Tag.Queries.GetTags;
using NextFlix.Application.Features.Tag.Queries.TagIsExist;
using NextFlix.Application.Features.Tag.Queries.TagSlugIsExist;
using NextFlix.Domain.Enums;

namespace NextFlix.API.Controllers
{
	[Route("tags")]
	[ApiController]
	

	public class TagController(IMediator mediator, IMapper mapper) : ControllerBase
	{


		[HttpGet("{id}")]
		public async Task<IActionResult> GetTag(int id)
		{
			GetTagQueryRequest request = new(id);
			var response = await mediator.Send(request);
			return Ok(response);
		}



		[HttpGet("exist")]
		public async Task<IActionResult> TagIsExist([FromQuery] int? id, [FromQuery] string? slug, [FromQuery] Status? status)
		{
			if (id.HasValue)
			{
				TagIsExistQueryRequest request = new(id.Value, status);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);

			}
			else
			{
				TagSlugIsExistQueryRequest request = new(slug, status);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);
			}
		}



		[HttpGet]
		public async Task<IActionResult> GetTag([FromQuery] TagFilterModel model)
		{
			GetTagsQueryRequest request = mapper.Map<GetTagsQueryRequest>(model);
			var response = await mediator.Send(request);
			return Ok(response);
		}


		[HttpPost]
		public async Task<IActionResult> CreateTag([FromForm] TagDto model)
		{
			CreateTagCommandRequest request = mapper.Map<CreateTagCommandRequest>(model);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTag(int id, [FromForm] TagDto model)
		{
			UpdateTagCommandRequest request = mapper.Map<UpdateTagCommandRequest>(model);
			request.Id = id;
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTag(int id)
		{

			DeleteTagCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteTag([FromBody] List<int> id)
		{
			DeleteTagCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

	}

}
