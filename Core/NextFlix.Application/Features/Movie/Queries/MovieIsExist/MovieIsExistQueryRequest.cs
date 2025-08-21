using NextFlix.Application.Interfaces;
using NextFlix.Domain.Enums;

namespace NextFlix.Application.Features.Movie.Queries.MovieIsExist
{
	public class MovieIsExistQueryRequest(int id, Status? status) : IIsExistRequest
	{
		public int Id { get; set; } = id;
		public Status? Status { get; set; } = status;
	}
}
