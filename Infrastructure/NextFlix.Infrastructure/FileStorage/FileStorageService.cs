using NextFlix.Application.Abstraction.Interfaces.FileStorage;

namespace NextFlix.Infrastructure.FileStorage
{
	public class FileStorageService: IFileStorageService
	{
	

		public async Task<string> SaveFileAsync(Stream content, string fileName,string webRootPath,string folderName="",CancellationToken cancellationToken=default)
		{
			var folderPath = Path.Combine(webRootPath, "images",folderName);
			string newFileName = $"{Guid.NewGuid()}_{fileName}";
			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			var filePath = Path.Combine(folderPath, newFileName);
			await using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				await content.CopyToAsync(fileStream,cancellationToken);
			}
			if(string.IsNullOrEmpty(folderName))
				return $"/images/{newFileName}";
			return $"/images/{folderName}/{newFileName}";
		}
	}
}
