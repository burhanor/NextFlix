using FluentValidation;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Helpers
{
	public static class ResponseContainerHelper
	{
		public static async Task<ResponseContainer<TResponse>> Validate<TResponse,TValidator,TRequest>(TRequest request,CancellationToken cancellationToken)
		where TValidator : AbstractValidator<TRequest>, new()
		{
			ResponseContainer<TResponse> response = new ResponseContainer<TResponse>();
			TValidator validationRules = new();
			var validationResult = await validationRules.ValidateAsync(request, cancellationToken);
			if (!validationResult.IsValid)
			{
				response.ValidationErrors = validationResult.Errors.Select(x => new ValidationError
				{
					ErrorMessage = x.ErrorMessage,
					PropertyName = x.PropertyName
				}).ToList();
				response.Status = ResponseStatus.ValidationError;
				return response;
			}
			return response;
		}
	}
}
