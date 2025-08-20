using AutoMapper;
using NextFlix.Application.Dto.TagDtos;
using NextFlix.Application.Features.Tag.Commands.CreateTag;
using NextFlix.Application.Features.Tag.Commands.UpdateTag;
using NextFlix.Application.Features.Tag.Queries.GetTag;
using NextFlix.Application.Features.Tag.Queries.GetTags;

namespace NextFlix.Application.Mappings
{
	public class TagMappingProfile:Profile
	{
		public TagMappingProfile()
		{
			CreateMap<TagDto, CreateTagCommandRequest>();
			CreateMap<CreateTagCommandRequest, Domain.Entities.Tag>();
			CreateMap<Domain.Entities.Tag, CreateTagCommandResponse>();

			CreateMap<TagDto, UpdateTagCommandRequest>();
			CreateMap<UpdateTagCommandRequest, Domain.Entities.Tag>();
			CreateMap<Domain.Entities.Tag, UpdateTagCommandResponse>();


			CreateMap<Domain.Entities.Tag, GetTagQueryResponse>();

			CreateMap<TagFilterModel, GetTagsQueryRequest>();
			CreateMap<Domain.Entities.Tag, GetTagsQueryRequest>();
			CreateMap<Domain.Entities.Tag, GetTagsQueryResponse>();
			CreateMap<Domain.Entities.Tag, GetTagQueryResponse>();

		}
	}
}
