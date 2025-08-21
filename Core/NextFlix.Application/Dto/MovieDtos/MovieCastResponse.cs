using NextFlix.Application.Dto.CastDtos;

namespace NextFlix.Application.Dto.MovieDtos
{
	public class MovieCastResponse:CastResponse
	{
		public byte DisplayOrder { get; set; }
	}
}
