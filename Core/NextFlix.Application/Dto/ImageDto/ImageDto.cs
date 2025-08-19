namespace NextFlix.Application.Dto.ImageDto
{
	public class ImageDto
	{
		public string ContentType { get; set; }
		public string FileName { get; set; }
		public Stream Stream { get; set; }
		public string WebRootPath { get; set; }
	}
}
