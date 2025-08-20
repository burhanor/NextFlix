using NextFlix.Application.Interfaces;
using NextFlix.Domain.Enums;

namespace NextFlix.Application.Features.Source.Queries.SourceIsExist
{
	public class SourceIsExistQueryRequest(int id, Status? status) : IIsExistRequest
	{
		public int Id { get; set; } = id;
		public Status? Status { get; set; } = status;
	}
}
