namespace NextFlix.Shared.Response
{
	public class ResponseContainer<T>
	{
		public ResponseStatus Status { get; set; }
		public string? Message { get; set; }
		public T? Data { get; set; }
		public List<ValidationError>? ValidationErrors { get; set; }
		public ResponseContainer()
		{
			
		}
		public ResponseContainer(ResponseStatus status)
		{
			Status = status;
		}
		public ResponseContainer(ResponseStatus status,string message)
		{
			Status = status;
			Message = message;
		}
	}

}
