using NextFlix.Application.Dto.TagDtos;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Tag.Commands.UpdateTag
{
	public class UpdateTagCommandRequest : TagDto, IRequestContainer<UpdateTagCommandResponse>
	{
		public int Id { get; set; }
	}
}
