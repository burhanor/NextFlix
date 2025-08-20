using FluentValidation;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Tag.Commands.CreateTag
{


	public class CreateTagCommandValidator : AbstractValidator<CreateTagCommandRequest>
	{
		public CreateTagCommandValidator()
		{
			RuleFor(m => m.Name)
				.NotEmpty().WithMessage(TagMessages.NAME_REQUIRED)
				.MaximumLength(50).WithMessage(TagMessages.NAME_MAX_LENGTH);
			RuleFor(m => m.Slug)
				.NotEmpty().WithMessage(TagMessages.SLUG_REQUIRED)
				.MaximumLength(50).WithMessage(TagMessages.SLUG_MAX_LENGTH);
			RuleFor(m => m.Status)
				.IsInEnum().WithMessage(CommonMessages.STATUS_INVALID);
		}
	}
}
