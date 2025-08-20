using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextFlix.API.Extensions;
using NextFlix.Application.Dto.CategoryDtos;
using NextFlix.Application.Features.Category.Commands.CreateCategory;
using NextFlix.Application.Features.Category.Commands.DeleteCategory;
using NextFlix.Application.Features.Category.Commands.UpdateCategory;
using NextFlix.Application.Features.Category.Queries.CategoryIsExist;
using NextFlix.Application.Features.Category.Queries.CategorySlugIsExist;
using NextFlix.Application.Features.Category.Queries.GetCategories;
using NextFlix.Application.Features.Category.Queries.GetCategory;
using NextFlix.Domain.Enums;

namespace NextFlix.API.Controllers
{
	[Route("categories")]
	[ApiController]
	


	public class CategoryController(IMediator mediator, IWebHostEnvironment environment, IMapper mapper) : ControllerBase
	{


		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategory(int id)
		{
			GetCategoryQueryRequest request = new(id);
			var response = await mediator.Send(request);
			return Ok(response);
		}



		[HttpGet("exist")]
		public async Task<IActionResult> CategoryIsExist([FromQuery] int? id, [FromQuery] string? slug, [FromQuery] Status? status)
		{
			if (id.HasValue)
			{
				CategoryIsExistQueryRequest request = new(id.Value, status);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);

			}
			else
			{
				CategorySlugIsExistQueryRequest request = new(slug, status);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);
			}
		}



		[HttpGet]
		public async Task<IActionResult> GetCategory([FromQuery] CategoryFilterModel model)
		{
			GetCategoriesQueryRequest request = mapper.Map<GetCategoriesQueryRequest>(model);
			var response = await mediator.Send(request);
			return Ok(response);
		}


		[HttpPost]
		public async Task<IActionResult> CreateCategory([FromForm] CategoryDto model)
		{
			CreateCategoryCommandRequest request = mapper.Map<CreateCategoryCommandRequest>(model);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id, [FromForm] CategoryDto model)
		{
			UpdateCategoryCommandRequest request = mapper.Map<UpdateCategoryCommandRequest>(model);
			request.Id = id;
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{

			DeleteCategoryCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCategory([FromBody] List<int> id)
		{
			DeleteCategoryCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

	}
}
