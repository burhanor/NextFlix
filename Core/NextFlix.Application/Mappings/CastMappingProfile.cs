using AutoMapper;
using NextFlix.Application.Dto.CastDtos;
using NextFlix.Application.Features.Cast.Commands.CreateCast;
using NextFlix.Application.Features.Cast.Commands.UpdateCast;
using NextFlix.Application.Features.Cast.Queries.GetCast;
using NextFlix.Application.Features.Cast.Queries.GetCasts;

namespace NextFlix.Application.Mappings
{
	internal class CastMappingProfile:Profile
	{
		public CastMappingProfile()
		{
			CreateMap<CastDto, CreateCastCommandRequest>();
			CreateMap<CreateCastCommandRequest, Domain.Entities.Cast>();
			CreateMap<Domain.Entities.Cast, CreateCastCommandResponse>();

			CreateMap<CastDto, UpdateCastCommandRequest>();
			CreateMap<UpdateCastCommandRequest, Domain.Entities.Cast>();
			CreateMap<Domain.Entities.Cast, UpdateCastCommandResponse>();


			CreateMap<Domain.Entities.Cast, GetCastQueryResponse>();

			CreateMap<CastFilterModel, GetCastsQueryRequest>();
			CreateMap<Domain.Entities.Cast, GetCastsQueryResponse>();
			CreateMap<Domain.Entities.Cast, GetCastQueryResponse>();

		}
	}
}
