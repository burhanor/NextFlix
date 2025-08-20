using System.Text.RegularExpressions;

namespace NextFlix.Application.Extensions
{
	public static class StringExtension
	{
		public static string ToSlug(this string str,int maxLength=46)
		{
			if (string.IsNullOrEmpty(str))
				return string.Empty;
			str = str.ToLowerInvariant();
			str = str.Replace(" ", "-");
			str = Regex.Replace(str, @"[^a-z0-9\-]", string.Empty);
			str = Regex.Replace(str, @"-+", "-").Trim('-');
			string suffix = Guid.NewGuid().ToString("N").Substring(0, 4);
			if (str.Length > maxLength)
			{
				str = str.Substring(0, maxLength);
				str = str.TrimEnd('-');
			}
			str += $"-{suffix}";

			return str;
		}
	}
}
