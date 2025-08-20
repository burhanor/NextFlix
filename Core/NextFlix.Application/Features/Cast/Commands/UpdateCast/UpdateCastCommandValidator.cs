using FluentValidation;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Cast.Commands.UpdateCast
{
	internal class UpdateCastCommandValidator:AbstractValidator<UpdateCastCommandRequest>
	{
		public UpdateCastCommandValidator()
		{
			RuleFor(m => m.Name)
			.NotEmpty().WithMessage(CastMessages.NAME_REQUIRED)
			.MaximumLength(50).WithMessage(CastMessages.NAME_MAX_LENGTH);
			RuleFor(m => m.Slug)
				.NotEmpty().WithMessage(CastMessages.SLUG_REQUIRED)
				.MaximumLength(50).WithMessage(CastMessages.SLUG_MAX_LENGTH);
			RuleFor(m => m.Status)
				.IsInEnum().WithMessage(CommonMessages.STATUS_INVALID);
			RuleFor(m => m.CastType)
			.IsInEnum().WithMessage(CastMessages.CAST_TYPE_INVALID);
			RuleFor(m => m.Gender)
			.IsInEnum().WithMessage(CastMessages.GENDER_INVALID);
		}
	}
}
