namespace NextFlix.Application.Abstraction.Interfaces.FileStorage
{
	public interface IFileStorageService
	{
		Task<string> SaveFileAsync(Stream content, string fileName, string webRootPath, CancellationToken cancellationToken=default);
	}
}
