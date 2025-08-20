using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.User.Commands.CreateUser
{
	

	public class CreateUserCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService, IFileStorageService fileStorageService) : BaseHandler<Domain.Entities.User>(uow, mapper, rabbitMqService), IRequestHandler<CreateUserCommandRequest, ResponseContainer<CreateUserCommandResponse>>
	{
		public async Task<ResponseContainer<CreateUserCommandResponse>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<CreateUserCommandResponse> response = await ResponseContainerHelper.Validate<CreateUserCommandResponse, CreateUserCommandValidator, CreateUserCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug, cancellationToken);
			if (isSlugExists)
			{
				response.ValidationErrors =
				[
					new ValidationError
					{
						ErrorMessage = UserMessages.SLUG_ALREADY_EXISTS,
						PropertyName = nameof(request.Slug)
					}
				];
				return response;
			}
			bool isNicknameExists = await readRepository.ExistAsync(x => x.Nickname == request.Nickname, cancellationToken);
			if (isNicknameExists)
			{
				response.ValidationErrors =
				[
					new ValidationError
					{
						ErrorMessage = UserMessages.NICKNAME_ALREADY_EXISTS,
						PropertyName = nameof(request.Nickname)
					}
				];
				return response;
			}

			bool isEmailExists = await readRepository.ExistAsync(x => x.EmailAddress == request.EmailAddress, cancellationToken);
			if (isEmailExists)
			{
				response.ValidationErrors =
				[
					new ValidationError
					{
						ErrorMessage = UserMessages.EMAIL_ALREADY_EXISTS,
						PropertyName = nameof(request.EmailAddress)
					}
				];
				return response;
			}

			

			Domain.Entities.User user = mapper.Map<Domain.Entities.User>(request);
			user.CreatedDate = DateTime.Now;
			user.Password = PasswordHelper.HashPassword(request.Password);
			if (request.AvatarImage != null)
			{
				user.Avatar = await fileStorageService.SaveFileAsync(request.AvatarImage.Stream, request.AvatarImage.FileName, request.AvatarImage.WebRootPath, cancellationToken);
			}
			await writeRepository.AddAsync(user, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (user.Id > 0)
			{
				response.Status = ResponseStatus.Created;
				response.Message = UserMessages.CREATED;
				response.Data = mapper.Map<CreateUserCommandResponse>(user);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Users, Abstraction.Enums.RabbitMqRoutingKeys.Created, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = UserMessages.CREATED_ERROR;
			}
			return response;
		}


	}
}
