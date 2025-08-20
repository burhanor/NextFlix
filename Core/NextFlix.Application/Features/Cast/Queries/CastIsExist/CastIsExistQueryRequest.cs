using NextFlix.Application.Interfaces;
using NextFlix.Domain.Enums;

namespace NextFlix.Application.Features.Cast.Queries.CastIsExist
{
	public class CastIsExistQueryRequest(int id, Status? status) : IIsExistRequest
	{
		public int Id { get; set; } = id;
		public Status? Status { get; set; } = status;
	}
}
