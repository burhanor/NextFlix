using NextFlix.Application.Dto.SourceDtos;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Source.Commands.UpdateSource
{
	public class UpdateSourceCommandRequest : SourceDto, IRequestContainer<UpdateSourceCommandResponse>
	{
		public int Id { get; set; }
	}
}
