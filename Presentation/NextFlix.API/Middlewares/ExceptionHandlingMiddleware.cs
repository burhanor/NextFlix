using Microsoft.AspNetCore.Http;
using System.Text;

namespace NextFlix.API.Middlewares
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;

		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context); // Request pipeline'ı devam ettir
			}
			catch (Exception ex)
			{


				var endpoint = context.GetEndpoint();
				var routeData = context.GetRouteData();

				string controller = routeData?.Values["controller"]?.ToString() ?? "UnknownController";
				string action = routeData?.Values["action"]?.ToString() ?? "UnknownAction";

				context.Request.EnableBuffering();
				using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
				var body = await reader.ReadToEndAsync();
				context.Request.Body.Position = 0;

				var queryParams = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : "{}";

				StringBuilder sb = new();
				sb.AppendLine($"Time: {DateTime.UtcNow}");
				sb.AppendLine($"Controller: {controller}");
				sb.AppendLine($"Action: {action}");
				sb.AppendLine($"Query: {queryParams}");
				sb.AppendLine($"Body: {body}");
				sb.AppendLine($"User: {context.User?.Identity?.Name ?? "Anonymous"}");
				sb.AppendLine($"Endpoint: {endpoint?.DisplayName ?? "Unknown"}");
				sb.AppendLine($"IP Address: {context.Connection.RemoteIpAddress?.ToString() ?? "Unknown"}");
				sb.AppendLine($"Request Method: {context.Request.Method}");
				sb.AppendLine($"Request Path: {context.Request.Path}");
				sb.AppendLine($"Request Headers: {string.Join(", ", context.Request.Headers.Select(h => $"{h.Key}: {h.Value}"))}");
				sb.AppendLine($"Response Headers: {string.Join(", ", context.Response.Headers.Select(h => $"{h.Key}: {h.Value}"))}");
				sb.AppendLine($"Exception: {ex.Message}");
				sb.AppendLine($"Stack Trace: {ex.StackTrace}");
				sb.AppendLine("--------------------------------------------------");


				_logger.LogError(sb.ToString());
				if (context.Response.Body.CanWrite == false)
				{
					context.Response.Body = new MemoryStream();
				}
				context.Response.Clear();
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;
				context.Response.ContentType = "application/json";
				

				var response = new
				{
					StatusCode = context.Response.StatusCode,
					Message = "Sunucuda beklenmeyen bir hata oluştu."
				};

				await context.Response.WriteAsJsonAsync(response);

			}
		}
	}
}
