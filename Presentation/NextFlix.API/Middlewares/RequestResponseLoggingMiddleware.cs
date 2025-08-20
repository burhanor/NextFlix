using NextFlix.API.Attributes;
using System.Text;

namespace NextFlix.API.Middlewares
{
	public class RequestResponseLoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

		public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{

			var endpoint = context.GetEndpoint();
			var hasLoggingAttribute = endpoint?.Metadata
				.GetMetadata<RequestResponseLogAttribute>() != null;

			if (!hasLoggingAttribute)
			{
				await _next(context); 
				return;
			}

			context.Request.EnableBuffering();
			string requestBody = "";
			if (context.Request.ContentLength > 0 &&
				(context.Request.ContentType?.Contains("application/json") == true ||
				 context.Request.ContentType?.Contains("text/") == true))
			{
				context.Request.EnableBuffering(); 
				using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
				requestBody = await reader.ReadToEndAsync();
				context.Request.Body.Position = 0;
			}
			else if (context.Request.ContentType?.Contains("multipart/form-data") == true)
			{
				var form = await context.Request.ReadFormAsync();

				var fileNames = form.Files.Select(f => f.FileName).ToList();

				var formFields = form
				   .Where(f => f.Value.Count > 0)
				   .ToDictionary(f => f.Key, f => string.Join(", ", f.Value.ToArray()));

				var sb2 = new StringBuilder();
				if (formFields.Any())
				{
					foreach (var kvp in formFields)
					{
						sb2.AppendLine($"{kvp.Key}: {kvp.Value}");
					}
				}
				if (fileNames.Any())
				{
					sb2.AppendLine("Files: " + string.Join(", ", fileNames));
				}

				requestBody = sb2.ToString();
			}
			var originalBodyStream = context.Response.Body;
			using var responseBody = new MemoryStream();
			context.Response.Body = responseBody;

			await _next(context);

			context.Response.Body.Seek(0, SeekOrigin.Begin);
			string responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
			context.Response.Body.Seek(0, SeekOrigin.Begin);

			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"Request Time: {DateTime.UtcNow}");
			sb.AppendLine($"Request Method: {context.Request.Method}");
			sb.AppendLine($"Request Path: {context.Request.Path}");
			sb.AppendLine($"Request Query: {context.Request.QueryString.Value}");
			sb.AppendLine($"Request Body: {requestBody}");
			sb.AppendLine($"Response Status Code: {context.Response.StatusCode}");
			sb.AppendLine($"Response Body: {responseBodyText}");
			sb.AppendLine($"Request IP: {context.Connection.RemoteIpAddress}");
			sb.AppendLine($"Request User Agent: {context.Request.Headers["User-Agent"]}");
			sb.AppendLine($"Request Path Base: {context.Request.PathBase}");
			sb.AppendLine("---------------------------------------------");
			_logger.LogInformation(sb.ToString());

			await responseBody.CopyToAsync(originalBodyStream);
		}
	}

}
