using FluentValidation;
using NextFlix.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextFlix.Application.Features.Movie.Commands.UpdateMovie
{
	internal class UpdateMovieCommandValidator:AbstractValidator<UpdateMovieCommandRequest>
	{
		public UpdateMovieCommandValidator()
		{
			RuleFor(m => m.Title)
				.NotEmpty().WithMessage(MovieMessages.TITLE_REQUIRED)
				.MaximumLength(200).WithMessage(MovieMessages.TITLE_MAX_LENGTH);
			RuleFor(m => m.Description)
				.MaximumLength(1000).WithMessage(MovieMessages.DESCRIPTION_MAX_LENGTH);
			RuleFor(m => m.Status)
				.IsInEnum().WithMessage(CommonMessages.STATUS_INVALID);
			RuleFor(m => m.Duration)
				.NotEmpty().WithMessage(MovieMessages.DURATION_REQUIRED)
				.GreaterThan(0).WithMessage(MovieMessages.DURATION_INVALID);
		}
	}
}
