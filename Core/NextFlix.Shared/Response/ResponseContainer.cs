namespace NextFlix.Shared.Response
{
	public class ResponseContainer<T>
	{
		public Status Status { get; set; }
		public string? Message { get; set; }
		public T? Data { get; set; }
		public List<ValidationError>? ValidationErrors { get; set; }

	}
}
