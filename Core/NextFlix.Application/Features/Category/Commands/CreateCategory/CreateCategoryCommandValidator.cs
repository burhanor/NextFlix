using FluentValidation;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Category.Commands.CreateCategory
{
	public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommandRequest>
	{
		public CreateCategoryCommandValidator()
		{
			RuleFor(m => m.Name)
				.NotEmpty().WithMessage(CategoryMessages.NAME_REQUIRED)
				.MaximumLength(50).WithMessage(CategoryMessages.NAME_MAX_LENGTH);
			RuleFor(m => m.Slug)
				.NotEmpty().WithMessage(CategoryMessages.SLUG_REQUIRED)
				.MaximumLength(50).WithMessage(CategoryMessages.SLUG_MAX_LENGTH);
			RuleFor(m => m.Status)
				.IsInEnum().WithMessage(CommonMessages.STATUS_INVALID);
		}
	}
}
