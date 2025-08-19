using Microsoft.AspNetCore.Mvc;
using NextFlix.Shared.Response;

namespace NextFlix.API.Extensions
{
	public static class ResponseExtension
	{
		public static  IActionResult ToApiResponse<T>(this ControllerBase controller, ResponseContainer<T> response)
		{
			if (response.ValidationErrors?.Count > 0)
			{
				response.Status = ResponseStatus.ValidationError;
				return controller.BadRequest(response);
			}
			if (response.Status == ResponseStatus.NotFound)
			{
				return controller.NotFound(response);
			}
			if (response.Status == ResponseStatus.BadRequest)
			{
				return controller.BadRequest(response);
			}
			if (response.Status == ResponseStatus.Created)
			{
				return controller.StatusCode(StatusCodes.Status201Created, response);
			}
			if (response.Status == ResponseStatus.Deleted)
			{
				return controller.NoContent();
			}
			if (response.Status == ResponseStatus.Unauthorized)
			{
				return controller.Unauthorized();
			}
			if (response.Status == ResponseStatus.Forbidden)
			{
				return controller.Forbid();
			}
			if (response.Status == ResponseStatus.InternalServerError)
			{
				return controller.StatusCode(StatusCodes.Status500InternalServerError, response);
			}
			return controller.Ok(response);
		}
	}
}
