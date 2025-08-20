using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Channel.Queries.ChannelIsExist
{
	public class ChannelIsExistQueryHandler(IUow uow) : IsExistHandler<Domain.Entities.Channel, ChannelIsExistQueryRequest>(uow)
	{
	}
}
