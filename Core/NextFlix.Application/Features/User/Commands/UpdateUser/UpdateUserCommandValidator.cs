using FluentValidation;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.User.Commands.UpdateUser
{
	internal class UpdateUserCommandValidator:AbstractValidator<UpdateUserCommandRequest>
	{
		public UpdateUserCommandValidator()
		{
			RuleFor(m => m.Nickname)
				.NotEmpty().WithMessage(UserMessages.NICKNAME_REQUIRED)
				.MaximumLength(50).WithMessage(UserMessages.NICKNAME_MAX_LENGTH);
			RuleFor(m => m.EmailAddress)
				.NotEmpty().WithMessage(UserMessages.EMAIL_REQUIRED)
				.EmailAddress().WithMessage(UserMessages.EMAIL_INVALID)
				.MaximumLength(100).WithMessage(UserMessages.EMAIL_MAX_LENGTH);
			RuleFor(m => m.Slug)
				.NotEmpty().WithMessage(UserMessages.SLUG_REQUIRED)
				.MaximumLength(50).WithMessage(UserMessages.SLUG_MAX_LENGTH);
			RuleFor(m => m.UserType)
				.IsInEnum().WithMessage(UserMessages.USER_TYPE_INVALID);
		}
	}
}
