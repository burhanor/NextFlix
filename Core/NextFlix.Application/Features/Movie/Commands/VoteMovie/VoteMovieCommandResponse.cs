using NextFlix.Domain.Enums;

namespace NextFlix.Application.Features.Movie.Commands.VoteMovie
{
	public class VoteMovieCommandResponse
	{
		public VoteType Vote { get; set; }
		public int Count { get; set; }
	}
}
