using NextFlix.Application.Abstraction.Interfaces.FileStorage;

namespace NextFlix.Infrastructure.FileStorage
{
	public class FileStorageService: IFileStorageService
	{
	

		public async Task<string> SaveFileAsync(Stream content, string fileName,string webRootPath,CancellationToken cancellationToken=default)
		{
			var folderPath = Path.Combine(webRootPath, "images");
			string newFileName = $"{Guid.NewGuid()}_{fileName}";
			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			var filePath = Path.Combine(folderPath, newFileName);
			await using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				await content.CopyToAsync(fileStream,cancellationToken);
			}
			return $"/images/{newFileName}";
		}
	}
}
