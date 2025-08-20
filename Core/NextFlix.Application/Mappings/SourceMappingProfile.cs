using AutoMapper;
using NextFlix.Application.Dto.SourceDtos;
using NextFlix.Application.Features.Source.Commands.CreateSource;
using NextFlix.Application.Features.Source.Commands.UpdateSource;
using NextFlix.Application.Features.Source.Queries.GetSource;
using NextFlix.Application.Features.Source.Queries.GetSources;

namespace NextFlix.Application.Mappings
{
	public class SourceMappingProfile:Profile
	{
		public SourceMappingProfile()
		{
			CreateMap<SourceDto, CreateSourceCommandRequest>();
			CreateMap<CreateSourceCommandRequest, Domain.Entities.Source>();
			CreateMap<Domain.Entities.Source, CreateSourceCommandResponse>();

			CreateMap<SourceDto, UpdateSourceCommandRequest>();
			CreateMap<UpdateSourceCommandRequest, Domain.Entities.Source>();
			CreateMap<Domain.Entities.Source, UpdateSourceCommandResponse>();


			CreateMap<Domain.Entities.Source, GetSourceQueryResponse>();

			CreateMap<SourceFilterModel, GetSourcesQueryRequest>();
			CreateMap<Domain.Entities.Source, GetSourcesQueryRequest>();
			CreateMap<Domain.Entities.Source, GetSourcesQueryResponse>();
			CreateMap<Domain.Entities.Source, GetSourceQueryResponse>();

		}
	}
}
