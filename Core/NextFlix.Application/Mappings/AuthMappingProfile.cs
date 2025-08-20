using AutoMapper;
using NextFlix.Application.Dto.AuthDtos;
using NextFlix.Application.Features.Auth.Commands.Login;
using NextFlix.Application.Features.Auth.Commands.Register;
using NextFlix.Shared.Models;

namespace NextFlix.Application.Mappings
{
	internal class AuthMappingProfile:Profile
	{
		public AuthMappingProfile()
		{
			CreateMap<Domain.Entities.User, UserModel>();
			CreateMap<Domain.Entities.Login, LoginCommandResponse>();
			CreateMap<LoginDto, LoginCommandRequest>();
			CreateMap<RegisterDto, RegisterCommandRequest>();


		}
	}
}
