using Microsoft.Extensions.Options;
using NextFlix.Application.Abstraction.Interfaces.Redis;
using StackExchange.Redis;
using System.Text.Json;

namespace NextFlix.Infrastructure.Redis
{
	public class RedisService:IRedisService
	{

		private readonly ConnectionMultiplexer _redis;
		private readonly IDatabase _db;
		private readonly RedisModel _options;

		public RedisService(IOptions<RedisModel> options)
		{
			_options = options.Value;
			var config = new ConfigurationOptions
			{
				EndPoints = { $"{_options.Host}:{_options.Port}" },
				Password = _options.Password
			};

			const int delayMilliseconds = 2000;
			while (true)
			{
				try
				{
					Console.WriteLine($"Redis'e bağlanılıyor: {_options.Host}:{_options.Port}");
					_redis = ConnectionMultiplexer.Connect(config);
					break;
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Redis bağlantısı başarısız: {ex.Message}");
					Thread.Sleep(delayMilliseconds);
				}

			}
			Console.WriteLine($"Redis'e bağlandı: {_options.Host}:{_options.Port}");

			_db = _redis.GetDatabase();
		}



		public async Task<string?> GetStringAsync(string key)
		{
			return await _db.StringGetAsync(key);
		}



		public async Task<T?> GetObjectAsync<T>(string key)
		{
			var json = await _db.StringGetAsync(key);
			return json.HasValue ? JsonSerializer.Deserialize<T>(json) : default;
		}


		public async Task<bool> ExistsAsync(string key)
		{
			return await _db.KeyExistsAsync(key);
		}


		public async Task<T?> GetAsync<T>(string hashKey, string id)
		{
			var json = await _db.HashGetAsync(hashKey, id);
			if (json.IsNullOrEmpty)
				return default;

			return JsonSerializer.Deserialize<T>(json);
		}

		public async Task<List<T>> GetAllAsync<T>(string hashKey)
		{
			var entries = await _db.HashGetAllAsync(hashKey);
			return entries
				.Select(e => JsonSerializer.Deserialize<T>(e.Value))
				.Where(x => x != null)
				.Select(x => x!)
				.ToList();
		}


		public async Task StringSetAsync<T>(string key, T value, TimeSpan? expiry = null)
		{
			var json = JsonSerializer.Serialize(value);
			await _db.StringSetAsync(key, json, expiry);
		}

		public async Task HashSetAsync<T>(string hashKey, string id, T value, TimeSpan? expiry = null)
		{
			var json = JsonSerializer.Serialize(value);
			await _db.HashSetAsync(hashKey, id, json);
			if (expiry.HasValue)
			{
				await _db.KeyExpireAsync(hashKey, expiry.Value);
			}
		}

	}
}
