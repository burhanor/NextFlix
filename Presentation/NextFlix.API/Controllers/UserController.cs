using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextFlix.API.Extensions;
using NextFlix.Application.Dto.UserDtos;
using NextFlix.Application.Features.User.Commands.CreateUser;
using NextFlix.Application.Features.User.Commands.DeleteUser;
using NextFlix.Application.Features.User.Commands.UpdateUser;
using NextFlix.Application.Features.User.Queries.UserIsExist;
using NextFlix.Application.Features.User.Queries.GetUsers;
using NextFlix.Application.Features.User.Queries.GetUser;
using NextFlix.Domain.Enums;
using NextFlix.Application.Features.User.Queries.UserSlugIsExist;

namespace NextFlix.API.Controllers
{
	[Route("users")]
	[ApiController]

	public class UserController(IMediator mediator, IWebHostEnvironment environment, IMapper mapper) : ControllerBase
	{


		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			GetUserQueryRequest request = new(id);
			var response = await mediator.Send(request);
			return Ok(response);
		}



		[HttpGet("exist")]
		public async Task<IActionResult> UserIsExist([FromQuery] int? id, [FromQuery] string? slug)
		{
			if (id.HasValue)
			{
				UserIsExistQueryRequest request = new(id.Value);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);

			}
			else
			{
				UserSlugIsExistQueryRequest request = new(slug);
				var response = await mediator.Send(request);
				return this.ToApiResponse(response);
			}
		}



		[HttpGet]
		public async Task<IActionResult> GetUser([FromQuery] UserFilterModel model)
		{
			GetUsersQueryRequest request = mapper.Map<GetUsersQueryRequest>(model);
			var response = await mediator.Send(request);
			return Ok(response);
		}


		[HttpPost]
		public async Task<IActionResult> CreateUser([FromForm] UserDto model, [FromForm] IFormFile? file)
		{
			CreateUserCommandRequest request = new()
			{
				Nickname = model.Nickname,
				Slug = model.Slug,
				EmailAddress = model.EmailAddress,
				Password = model.Password,
				IsActive = model.IsActive,
				UserType = model.UserType,

				AvatarImage = file.ToImageDto(environment.WebRootPath)
			};
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, [FromForm] UserDto model, [FromForm] IFormFile? file)
		{
			UpdateUserCommandRequest request = new()
			{
				Nickname = model.Nickname,
				Slug = model.Slug,
				EmailAddress = model.EmailAddress,
				Password = model.Password,
				IsActive = model.IsActive,
				UserType = model.UserType,
				Id = id,
				AvatarImage = file.ToImageDto(environment.WebRootPath)
			};
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{

			DeleteUserCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteUser([FromBody] List<int> id)
		{
			DeleteUserCommandRequest request = new(id);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

	}
}
