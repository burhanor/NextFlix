using AutoMapper;
using NextFlix.Application.Dto.CategoryDtos;
using NextFlix.Application.Features.Category.Commands.CreateCategory;
using NextFlix.Application.Features.Category.Commands.UpdateCategory;
using NextFlix.Application.Features.Category.Queries.GetCategories;
using NextFlix.Application.Features.Category.Queries.GetCategory;

namespace NextFlix.Application.Mappings
{
	internal class CategoryMappingProfile:Profile
	{
		public CategoryMappingProfile()
		{
			CreateMap<CategoryDto, CreateCategoryCommandRequest>();
			CreateMap<CreateCategoryCommandRequest, Domain.Entities.Category>();
			CreateMap<Domain.Entities.Category, CreateCategoryCommandResponse>();

			CreateMap<CategoryDto, UpdateCategoryCommandRequest>();
			CreateMap<UpdateCategoryCommandRequest, Domain.Entities.Category>();
			CreateMap<Domain.Entities.Category, UpdateCategoryCommandResponse>();


			CreateMap<Domain.Entities.Category, GetCategoryQueryResponse>();

			CreateMap<CategoryFilterModel, GetCategoriesQueryRequest>();
			CreateMap<Domain.Entities.Category, GetCategoriesQueryRequest>();
			CreateMap<Domain.Entities.Category, GetCategoriesQueryResponse>();
			CreateMap<Domain.Entities.Category, GetCategoryQueryResponse>();

		}
	}
}
