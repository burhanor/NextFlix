using NextFlix.Application.Dto.TagDtos;

namespace NextFlix.Application.Dto.MovieDtos
{
	public class MovieTagResponse:TagResponse
	{
		public byte DisplayOrder { get; set; }
	}
}
