using FluentValidation;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.User.Commands.CreateUser
{
	internal class CreateUserCommandValidator:AbstractValidator<CreateUserCommandRequest>
	{
		public CreateUserCommandValidator()
		{
			RuleFor(m => m.Nickname)
				.NotEmpty().WithMessage(UserMessages.NICKNAME_REQUIRED)
				.MaximumLength(50).WithMessage(UserMessages.NICKNAME_MAX_LENGTH);
			RuleFor(m => m.EmailAddress)
				.NotEmpty().WithMessage(UserMessages.EMAIL_REQUIRED)
				.MaximumLength(100).WithMessage(UserMessages.EMAIL_MAX_LENGTH);
			RuleFor(m => m.Password)
				.NotEmpty().WithMessage(UserMessages.PASSWORD_REQUIRED);
			RuleFor(m => m.Slug)
				.NotEmpty().WithMessage(UserMessages.SLUG_REQUIRED)
				.MaximumLength(50).WithMessage(UserMessages.SLUG_MAX_LENGTH);
			RuleFor(m => m.UserType)
				.IsInEnum().WithMessage(UserMessages.USER_TYPE_INVALID);
		}
	}
}
