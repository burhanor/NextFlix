using NextFlix.Application.Dto.CategoryDtos;

namespace NextFlix.Application.Dto.MovieDtos
{
	public class MovieCategoryResponse:CategoryResponse
	{
		public byte DisplayOrder { get; set; }
	}
}
