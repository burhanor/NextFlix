using NextFlix.Application.Dto.CategoryDtos;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Category.Commands.CreateCategory
{
	public class CreateCategoryCommandRequest : CategoryDto, IRequestContainer<CreateCategoryCommandResponse>
	{
	}
}
