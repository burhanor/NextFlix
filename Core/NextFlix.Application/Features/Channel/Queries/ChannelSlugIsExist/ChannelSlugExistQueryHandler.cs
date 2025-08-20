using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;

namespace NextFlix.Application.Features.Channel.Queries.ChannelSlugIsExist
{
	public class ChannelSlugExistQueryHandler(IUow uow) : SlugExistHandler<Domain.Entities.Channel, ChannelSlugExistQueryRequest>(uow)
	{

	}
}
