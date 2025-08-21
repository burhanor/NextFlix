using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Movie.Commands.DeleteMovie
{
	public class DeleteMovieCommandRequest:DeleteRequest
	{
		public DeleteMovieCommandRequest() : base()
		{

		}
		public DeleteMovieCommandRequest(int id) : base(id)
		{
		}
		public DeleteMovieCommandRequest(List<int> ids) : base(ids)
		{
		}
	}
}
