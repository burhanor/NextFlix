using NextFlix.Application.Dto.ImageDto;
using System;

namespace NextFlix.API.Extensions
{
	public static class IFormFileExtension
	{
		public static ImageDto? ToImageDto(this IFormFile? file,string webRootPath)
		{
			try
			{
				if (file is null)
					return null;
				return new ImageDto
				{
					FileName = file.FileName,
					ContentType = file.ContentType,
					Stream = file.OpenReadStream(),
					WebRootPath = webRootPath
				};
			}
			catch (Exception)
			{
				return null;
			}
			
		}
	}
}
