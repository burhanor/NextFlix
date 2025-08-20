using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Extensions;
using NextFlix.Application.Features.User.Commands.CreateUser;
using NextFlix.Application.Helpers;
using NextFlix.Domain.Enums;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Auth.Commands.Register
{
	public class RegisterCommandHandler(IUow uow, IMapper mapper,IRabbitMqService rabbitMqService,IFileStorageService fileStorageService) : BaseHandler<Domain.Entities.User>(uow, mapper), IRequestHandler<RegisterCommandRequest, ResponseContainer<RegisterCommandResponse>>
	{
		public async Task<ResponseContainer<RegisterCommandResponse>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<RegisterCommandResponse> response = await ResponseContainerHelper.Validate<RegisterCommandResponse, RegisterCommandValidator, RegisterCommandRequest>(request, cancellationToken);
			response.Message = AuthMessages.REGISTER_FAILED;
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			CreateUserCommandRequest createUserCommandRequest = new CreateUserCommandRequest()
			{
				IsActive = true,
				EmailAddress = request.EmailAddress,
				Nickname = request.Nickname,
				Password = request.Password,
				Slug = request.Nickname.ToSlug(),
				UserType = UserType.Member,

			};
			CreateUserCommandHandler createUserCommandHandler = new (uow,mapper,rabbitMqService,fileStorageService);
			ResponseContainer<CreateUserCommandResponse> createUserResponse = await createUserCommandHandler.Handle(createUserCommandRequest, cancellationToken);
			if (createUserResponse.Status == ResponseStatus.Created)
			{
				response.Status = ResponseStatus.Success;
				response.Message = AuthMessages.REGISTER_SUCCESS;
				response.Data = new RegisterCommandResponse()
				{
					EmailAddress = request.EmailAddress
				};
			}
			else
			{
				response.ValidationErrors = createUserResponse.ValidationErrors;
				response.Status = createUserResponse.Status;
				response.Message = createUserResponse.Message;
				
			}
			return response;
		}
	}
}
