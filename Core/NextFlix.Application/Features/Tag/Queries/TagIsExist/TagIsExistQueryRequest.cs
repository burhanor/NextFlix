using NextFlix.Application.Interfaces;
using NextFlix.Domain.Enums;

namespace NextFlix.Application.Features.Tag.Queries.TagIsExist
{
	public class TagIsExistQueryRequest(int id, Status? status) : IIsExistRequest
	{
		public int Id { get; set; } = id;
		public Status? Status { get; set; } = status;
	}
}
