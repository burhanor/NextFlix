using NextFlix.Application.Dto.TagDtos;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Tag.Commands.CreateTag
{
	public class CreateTagCommandRequest : TagDto, IRequestContainer<CreateTagCommandResponse>
	{
	}
}
