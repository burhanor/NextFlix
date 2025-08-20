using AutoMapper;
using NextFlix.Application.Dto.UserDtos;
using NextFlix.Application.Features.User.Commands.CreateUser;
using NextFlix.Application.Features.User.Commands.UpdateUser;
using NextFlix.Application.Features.User.Queries.GetUser;
using NextFlix.Application.Features.User.Queries.GetUsers;

namespace NextFlix.Application.Mappings
{
	internal class UserMappingProfile:Profile
	{
		public UserMappingProfile()
		{
			CreateMap<UserDto, CreateUserCommandRequest>();
			CreateMap<CreateUserCommandRequest, Domain.Entities.User>();
			CreateMap<Domain.Entities.User, CreateUserCommandResponse>();

			CreateMap<UserDto, UpdateUserCommandRequest>();
			CreateMap<UpdateUserCommandRequest, Domain.Entities.User>();
			CreateMap<Domain.Entities.User, UpdateUserCommandResponse>();


			CreateMap<Domain.Entities.User, GetUserQueryResponse>();

			CreateMap<UserFilterModel, GetUsersQueryRequest>();
			CreateMap<Domain.Entities.User, GetUsersQueryResponse>();
			CreateMap<Domain.Entities.User, GetUserQueryResponse>();

		}
	}
}
