using FluentValidation;
using NextFlix.Application.Constants;

namespace NextFlix.Application.Features.Channel.Commands.UpdateChannel
{

	internal class UpdateChannelCommandValidator : AbstractValidator<UpdateChannelCommandRequest>
	{
		public UpdateChannelCommandValidator()
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
