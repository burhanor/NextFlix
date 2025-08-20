using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextFlix.API.Attributes;
using NextFlix.API.Extensions;
using NextFlix.Application.Dto.SourceDtos;
using NextFlix.Application.Features.Source.Commands.CreateSource;
using NextFlix.Application.Features.Source.Commands.DeleteSource;
using NextFlix.Application.Features.Source.Commands.UpdateSource;
using NextFlix.Application.Features.Source.Queries.GetSource;
using NextFlix.Application.Features.Source.Queries.GetSources;
using NextFlix.Application.Features.Source.Queries.SourceIsExist;
using NextFlix.Domain.Enums;

namespace NextFlix.API.Controllers
{
	[Route("sources")]
	[ApiController]
	[RequestResponseLog]


	public class SourceController(IMediator mediator, IMapper mapper) : ControllerBase
	{


		[HttpGet("{id}")]
		public async Task<IActionResult> GetSource(int id)
		{
			GetSourceQueryRequest request = new(id);
			var response = await mediator.Send(request);
			return Ok(response);
		}



		[HttpGet("exist")]
		public async Task<IActionResult> SourceIsExist([FromQuery] int id,  [FromQuery] Status? status)
		{
			SourceIsExistQueryRequest request = new(id, status);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}



		[HttpGet]
		public async Task<IActionResult> GetSource([FromQuery] SourceFilterModel model)
		{
			GetSourcesQueryRequest request = mapper.Map<GetSourcesQueryRequest>(model);
			var response = await mediator.Send(request);
			return Ok(response);
		}


		[HttpPost]
		public async Task<IActionResult> CreateSource([FromForm] SourceDto model)
		{
			CreateSourceCommandRequest request = mapper.Map<CreateSourceCommandRequest>(model);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateSource(int id, [FromForm] SourceDto model)
		{
			UpdateSourceCommandRequest request = mapper.Map<UpdateSourceCommandRequest>(model);
			request.Id = id;
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSource(int id)
		{

			DeleteSourceCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteSource([FromBody] List<int> id)
		{
			DeleteSourceCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

	}

}
