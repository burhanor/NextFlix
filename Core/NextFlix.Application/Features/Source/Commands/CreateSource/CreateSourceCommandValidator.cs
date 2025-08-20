using FluentValidation;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Source.Commands.CreateSource
{
	internal class CreateSourceCommandValidator:AbstractValidator<CreateSourceCommandRequest>
	{
		public CreateSourceCommandValidator()
		{
			RuleFor(m => m.Title)
				.NotEmpty().WithMessage(SourceMessages.TITLE_REQUIRED)
				.MaximumLength(100).WithMessage(SourceMessages.TITLE_MAX_LENGTH);
			RuleFor(m => m.Status)
				.IsInEnum().WithMessage(CommonMessages.STATUS_INVALID);
			RuleFor(m => m.SourceType)
				.IsInEnum().WithMessage(SourceMessages.SOURCE_TYPE_INVALID);
		}
	}
}
