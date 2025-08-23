using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Features.User.Commands.UpdateUser;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextFlix.Application.Features.User.Commands.UpdateUser
{
	


	public class UpdateUserCommandHandler(IUow uow, IMapper mapper, IRabbitMqService rabbitMqService, IFileStorageService fileStorageService) : BaseHandler<Domain.Entities.User>(uow, mapper, rabbitMqService), IRequestHandler<UpdateUserCommandRequest, ResponseContainer<UpdateUserCommandResponse>>
	{
		public async Task<ResponseContainer<UpdateUserCommandResponse>> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<UpdateUserCommandResponse> response = await ResponseContainerHelper.Validate<UpdateUserCommandResponse, UpdateUserCommandValidator, UpdateUserCommandRequest>(request, cancellationToken);
			if (response.Status == ResponseStatus.ValidationError)
				return response;

			Domain.Entities.User? oldUser = await readRepository.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
			if (oldUser == null)
			{
				response.Status = ResponseStatus.NotFound;
				response.Message = UserMessages.NOT_FOUND;
				return response;
			}


			bool isSlugExists = await readRepository.ExistAsync(x => x.Slug == request.Slug && x.Id != request.Id, cancellationToken);
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


			bool isNicknameExists = await readRepository.ExistAsync(x => x.Nickname == request.Nickname && x.Id != request.Id, cancellationToken);
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

			bool isEmailExists = await readRepository.ExistAsync(x => x.EmailAddress == request.EmailAddress && x.Id != request.Id, cancellationToken);
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
			user.CreatedDate = oldUser.CreatedDate;
			if (!string.IsNullOrEmpty(request.Password))
			{
				user.Password = PasswordHelper.HashPassword(request.Password);
			}
			else
			{
				string? password = oldUser.Password;
			}
			if (request.AvatarImage != null)
			{
				user.Avatar = await fileStorageService.SaveFileAsync(request.AvatarImage.Stream, request.AvatarImage.FileName, request.AvatarImage.WebRootPath, "users", cancellationToken);
			}
			else
			{
				user.Avatar = oldUser.Avatar;
			}
			writeRepository.Update(user);
			await uow.SaveChangesAsync(cancellationToken);
			if (user.Id > 0)
			{
				response.Status = ResponseStatus.Updated;
				response.Message = UserMessages.UPDATED;
				response.Data = mapper.Map<UpdateUserCommandResponse>(user);
				await RabbitMqService.Publish(Abstraction.Enums.RabbitMqQueues.Users, Abstraction.Enums.RabbitMqRoutingKeys.Updated, response.Data, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = UserMessages.UPDATED_ERROR;
			}
			return response;
		}
	}
}
