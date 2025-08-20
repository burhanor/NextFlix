using Microsoft.Extensions.Options;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using NextFlix.Application.Abstraction.Enums;

namespace NextFlix.Infrastructure.RabbitMq
{
	public class RabbitMqService:IRabbitMqService
	{
		private readonly RabbitMqModel _options;
		private IConnection? _connection;
		private IChannel? _channel;
		private readonly SemaphoreSlim _lock = new(1, 1);
		private bool _connected;
		BasicProperties properties = new BasicProperties()
		{
			DeliveryMode = DeliveryModes.Persistent
		};
		public RabbitMqService(IOptions<RabbitMqModel> options)
		{
			_options = options.Value;
		}

		public async Task Connect(CancellationToken cancellationToken)
		{
			if (_connected)
				return;

			await _lock.WaitAsync(cancellationToken);
			try
			{
				if (_connected)
					return;

				var factory = new ConnectionFactory
				{
					HostName = _options.Host,
					Port = _options.Port,
					UserName = _options.Username,
					Password = _options.Password,
					VirtualHost = _options.VirtualHost,
					
				};

				_connection = await factory.CreateConnectionAsync(cancellationToken);
				_channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

				_connected = true;
				await InitializeQueueAndBinding();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"RabbitMQ connection error: {ex.Message}");
				throw; 
			}
			finally
			{
				_lock.Release();
			}
		}

		private async Task InitializeQueueAndBinding()
		{
			var tasks = new List<Task>();

			foreach (RabbitMqQueues exchange in Enum.GetValues(typeof(RabbitMqQueues)))
			{
				string exchangeName = exchange.ToString();
				string queueName = exchange.ToString();

				tasks.Add(InitializeQueueAsync(exchangeName, queueName));

				var routingKeys = GetRoutingKeysForExchange(exchange);

				foreach (var routingKey in routingKeys)
				{
					tasks.Add(InitializeBindAsync(exchangeName, queueName, routingKey));
				}
			}

			await Task.WhenAll(tasks);
		}
		private IEnumerable<string> GetRoutingKeysForExchange(RabbitMqQueues exchange)
		{
			return	Enum.GetValues(typeof(RabbitMqRoutingKeys))
									  .Cast<RabbitMqRoutingKeys>()
									  .Select(x => x.ToString());
		}
		private async Task InitializeQueueAsync(string exchangeName, string queueName)
		{
			await _channel.QueueDeclareAsync(queue: queueName,
								  durable: true,
								  exclusive: false,
								  autoDelete: false,
								  arguments: null);
			await _channel.ExchangeDeclareAsync(exchange: exchangeName,
									 type: ExchangeType.Direct,
									 durable: true,
									 autoDelete: false);


		}
		private async Task InitializeBindAsync(string exchangeName, string queueName,string routingKey) 
		{
			await _channel.QueueBindAsync(queue: queueName,
							   exchange: exchangeName,
							   routingKey: routingKey);
		}


		public async Task Publish(RabbitMqQueues exchange, RabbitMqRoutingKeys routingType, object message, CancellationToken cancellationToken)
		{
			string exchangeName = exchange.ToString();
			string routingKey = routingType.ToString();
			string queueName = exchange.ToString();

			if (!_connected || _channel is null)
			{
				await Connect(cancellationToken);
			}

			try
			{
				//await _channel.ExchangeDeclareAsync(exchangeName.ToString(), ExchangeType.Direct, durable: true, autoDelete: false, arguments: null, cancellationToken: cancellationToken);

				//if (!string.IsNullOrEmpty(queueName))
				//{
				//	await _channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null, cancellationToken: cancellationToken);
				//	await _channel.QueueBindAsync(queueName, exchangeName, routingKey, arguments: null, cancellationToken: cancellationToken);
				//}

				var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));



				await _channel.BasicPublishAsync(
					exchange: exchangeName,
					routingKey: routingKey,
					mandatory: false,
					basicProperties: properties,
					body: body,
					cancellationToken: cancellationToken
				);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Publish error: {ex.Message}");
				throw;
			}
		}

		public async ValueTask DisposeAsync()
		{
			if (_channel is not null)
				await _channel.DisposeAsync();

			if (_connection is not null)
				await _connection.DisposeAsync();

			_lock.Dispose();
		}
	}
}
