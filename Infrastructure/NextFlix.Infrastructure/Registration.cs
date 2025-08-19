using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Redis;
using NextFlix.Infrastructure.FileStorage;
using NextFlix.Infrastructure.RabbitMq;
using NextFlix.Infrastructure.Redis;

namespace NextFlix.Infrastructure
{
	public static class Registration
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			
			services.Configure<RabbitMqModel>(configuration.GetSection("RabbitMQ"));
			services.AddSingleton<IRabbitMqService, RabbitMqService>();

			services.Configure<RedisModel>(configuration.GetSection("Redis"));
			services.AddSingleton<IRedisService, RedisService>();

			services.AddScoped<IFileStorageService, FileStorageService>();


		}
	}
}
