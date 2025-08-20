using NextFlix.Application.Dto.CategoryDtos;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Category.Commands.UpdateCategory
{
	
	public class UpdateCategoryCommandRequest : CategoryDto, IRequestContainer<UpdateCategoryCommandResponse>
	{
		public int Id { get; set; }
	}
}
