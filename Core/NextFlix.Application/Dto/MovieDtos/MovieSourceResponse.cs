using NextFlix.Application.Dto.SourceDtos;

namespace NextFlix.Application.Dto.MovieDtos
{
	public class MovieSourceResponse:SourceResponse
	{
		public string Link { get; set; }
		public byte DisplayOrder { get; set; }
	}
}
