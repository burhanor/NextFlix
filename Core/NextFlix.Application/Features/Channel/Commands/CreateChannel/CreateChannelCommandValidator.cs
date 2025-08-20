using FluentValidation;
using NextFlix.Application.Constants;
using NextFlix.Application.Features.Country.Commands.CreateCountry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextFlix.Application.Features.Channel.Commands.CreateChannel
{

	internal class CreateChannelCommandValidator : AbstractValidator<CreateChannelCommandRequest>
	{
		public CreateChannelCommandValidator()
		{
			RuleFor(m => m.Name)
				.NotEmpty().WithMessage(ChannelMessages.NAME_REQUIRED)
				.MaximumLength(50).WithMessage(ChannelMessages.NAME_MAX_LENGTH);
			RuleFor(m => m.Slug)
				.NotEmpty().WithMessage(ChannelMessages.SLUG_REQUIRED)
				.MaximumLength(50).WithMessage(ChannelMessages.SLUG_MAX_LENGTH);
			RuleFor(m => m.Status)
				.IsInEnum().WithMessage(CommonMessages.STATUS_INVALID);

		}
	}
}
