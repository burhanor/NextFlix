using NextFlix.Application.Dto.SourceDtos;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Source.Commands.CreateSource
{
	public class CreateSourceCommandRequest : SourceDto, IRequestContainer<CreateSourceCommandResponse>
	{
	}
}
