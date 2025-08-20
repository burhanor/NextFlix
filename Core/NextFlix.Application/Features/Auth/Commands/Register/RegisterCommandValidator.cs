using FluentValidation;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Auth.Commands.Register
{
	internal class RegisterCommandValidator:AbstractValidator<RegisterCommandRequest>
	{
		public RegisterCommandValidator()
		{
			RuleFor(m => m.Nickname)
				.NotEmpty().WithMessage(UserMessages.NICKNAME_REQUIRED)
				.MaximumLength(50).WithMessage(UserMessages.NICKNAME_MAX_LENGTH);
			RuleFor(m => m.EmailAddress)
				.NotEmpty().WithMessage(UserMessages.EMAIL_REQUIRED)
				.EmailAddress().WithMessage(UserMessages.EMAIL_INVALID)
				.MaximumLength(100).WithMessage(UserMessages.EMAIL_MAX_LENGTH);
			RuleFor(m => m.Password)
				.NotEmpty().WithMessage(UserMessages.PASSWORD_REQUIRED);
			
		}
	}
}
