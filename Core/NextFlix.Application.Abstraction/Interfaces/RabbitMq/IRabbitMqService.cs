using NextFlix.Application.Abstraction.Enums;

namespace NextFlix.Application.Abstraction.Interfaces.RabbitMq
{
	public interface IRabbitMqService: IAsyncDisposable
	{
		Task Connect(CancellationToken cancellationToken);
		Task Publish(RabbitMqQueues exchange, RabbitMqRoutingKeys routingType, object message, CancellationToken cancellationToken);
	}
}
