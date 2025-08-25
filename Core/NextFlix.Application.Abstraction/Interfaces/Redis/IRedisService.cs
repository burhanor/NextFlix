namespace NextFlix.Application.Abstraction.Interfaces.Redis
{
	public interface IRedisService
	{
		Task<string?> GetStringAsync(string key);

		Task<T?> GetObjectAsync<T>(string key);
		Task<bool> ExistsAsync(string key);

		Task<T?> GetAsync<T>(string hashKey, string id);

		Task<List<T>> GetAllAsync<T>(string hashKey);
		Task StringSetAsync<T>(string key, T value, TimeSpan? expiry = null);
		Task HashSetAsync<T>(string hashKey, string id, T value, TimeSpan? expiry = null);
		Task<List<T>?> HashGetAsync<T>(string hashKey, List<int> ids);
		Task<T?> HashGetAsync<T>(string hashKey, int id);
	}
}
