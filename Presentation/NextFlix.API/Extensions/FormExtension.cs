using System.Text.Json;

namespace NextFlix.API.Extensions
{
	public static class FormExtension
	{
		public static T? GetJson<T>(this IFormCollection form, string key)
		{
			if (!form.TryGetValue(key, out var value) || string.IsNullOrWhiteSpace(value))
				return default;

			return JsonSerializer.Deserialize<T>(value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}

		

		public static void MapJsonListsFromForm<T>(this T target, IFormCollection form)
		{
			var listProps = typeof(T)
				.GetProperties()
				.Where(p => p.CanWrite
							&& p.PropertyType.IsGenericType
							&& p.PropertyType.GetGenericTypeDefinition() == typeof(List<>));

			foreach (var prop in listProps)
			{
				var key = prop.Name;
				var value = form[key].FirstOrDefault();
				if (string.IsNullOrEmpty(value)) continue;

				try
				{
					var deserialized = JsonSerializer.Deserialize(value, prop.PropertyType, new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					});

					prop.SetValue(target, deserialized);
				}
				catch
				{
				}
			}
		}
	}
}
