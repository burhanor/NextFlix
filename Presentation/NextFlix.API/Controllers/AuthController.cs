using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextFlix.API.Extensions;
using NextFlix.Application.Dto.AuthDtos;
using NextFlix.Application.Features.Auth.Commands.Login;
using NextFlix.Application.Features.Auth.Commands.Register;

namespace NextFlix.API.Controllers
{
	[Route("auth")]
	[ApiController]
	public class AuthController(IMediator mediator,IMapper mapper) : ControllerBase
	{
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromForm] LoginDto model) 
		{
			LoginCommandRequest request = mapper.Map<LoginCommandRequest>(model);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromForm] RegisterDto model)
		{
			RegisterCommandRequest request = mapper.Map<RegisterCommandRequest>(model);
			var response = await mediator.Send(request);
			return this.ToApiResponse(response);
		}
	}
}
