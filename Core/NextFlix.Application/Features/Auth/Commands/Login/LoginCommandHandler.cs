using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Token;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Application.Helpers;
using NextFlix.Shared.Models;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Auth.Commands.Login
{
	public class LoginCommandHandler(IUow uow, IMapper mapper,IHttpContextAccessor httpContextAccessor, ITokenService tokenService) : BaseHandler<Domain.Entities.User>(uow,httpContextAccessor, mapper), IRequestHandler<LoginCommandRequest, ResponseContainer<LoginCommandResponse>>
	{
		public async Task<ResponseContainer<LoginCommandResponse>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<LoginCommandResponse> response = new(ResponseStatus.Failed,AuthMessages.LOGIN_FAILED);
			Domain.Entities.User? user = await readRepository.GetAsync(x => (x.EmailAddress == request.Email || x.Nickname == request.Email) && x.Password == PasswordHelper.HashPassword(request.Password) && x.IsActive, cancellationToken: cancellationToken);
			if (user is null)
				return response;
			UserModel userModel = mapper.Map<UserModel>(user);
			string accessToken = tokenService.GenerateAccessToken(userModel);
			string refreshToken = tokenService.GenerateRefreshToken();

			Domain.Entities.Login login =new ()
			{
				UserId = user.Id,
				AccessToken = accessToken,
				RefreshToken = refreshToken,
				LoginDate = DateTime.Now,
				IpAddress=ipAddress
			};
			IWriteRepository<Domain.Entities.Login> writeRepository = uow.GetWriteRepository<Domain.Entities.Login>();
			await writeRepository.AddAsync(login, cancellationToken: cancellationToken);
			await uow.SaveChangesAsync(cancellationToken: cancellationToken);
			if (login.Id > 0)
			{
				LoginCommandResponse loginResponse = mapper.Map<LoginCommandResponse>(login);
				response.Data = loginResponse;
				response.Status = ResponseStatus.Success;
				response.Message = AuthMessages.LOGIN_SUCCESS;
			}


			return response;
		}
	}
}
