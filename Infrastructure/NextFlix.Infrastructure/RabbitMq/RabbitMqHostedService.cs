using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;

namespace NextFlix.Infrastructure.RabbitMq
{
	public class RabbitMqHostedService : IHostedService
	{
		private readonly IRabbitMqService _rabbitMqService;

		public RabbitMqHostedService(IRabbitMqService rabbitMqService)
		{
			_rabbitMqService = rabbitMqService;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await _rabbitMqService.Connect(cancellationToken); 
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
