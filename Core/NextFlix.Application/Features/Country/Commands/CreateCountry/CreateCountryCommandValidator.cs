using FluentValidation;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Country.Commands.CreateCountry
{
	public class CreateCountryCommandValidator:AbstractValidator<CreateCountryCommandRequest>
	{
		public CreateCountryCommandValidator()
		{
			RuleFor(m => m.Name)
				.NotEmpty().WithMessage(CountryMessages.NAME_REQUIRED)
				.MaximumLength(50).WithMessage(CountryMessages.NAME_MAX_LENGTH);
			RuleFor(m => m.Slug)
				.NotEmpty().WithMessage(CountryMessages.SLUG_REQUIRED)
				.MaximumLength(50).WithMessage(CountryMessages.SLUG_MAX_LENGTH);
			RuleFor(m=>m.Status)
				.IsInEnum().WithMessage(CommonMessages.STATUS_INVALID);

		}
	}
}
