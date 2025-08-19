using FluentValidation;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Country.Commands.UpdateCountry
{
	internal class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommandRequest>
	{
		public UpdateCountryCommandValidator()
		{
			RuleFor(m => m.Name)
				.NotEmpty().WithMessage(CountryMessages.NAME_REQUIRED)
				.MaximumLength(50).WithMessage(CountryMessages.NAME_MAX_LENGTH);
			RuleFor(m => m.Slug)
				.NotEmpty().WithMessage(CountryMessages.SLUG_REQUIRED)
				.MaximumLength(50).WithMessage(CountryMessages.SLUG_MAX_LENGTH);
			RuleFor(m => m.Status)
				.IsInEnum().WithMessage(CommonMessages.STATUS_INVALID);

		}
	}
}
