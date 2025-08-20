using AutoMapper;
using NextFlix.Application.Dto.ChannelDtos;
using NextFlix.Application.Features.Channel.Commands.CreateChannel;
using NextFlix.Application.Features.Channel.Commands.UpdateChannel;
using NextFlix.Application.Features.Channel.Queries.GetChannel;
using NextFlix.Application.Features.Channel.Queries.GetChannels;

namespace NextFlix.Application.Mappings
{
	internal class ChannelMappingProfile:Profile
	{
		public ChannelMappingProfile()
		{
			CreateMap<ChannelDto, CreateChannelCommandRequest>();
			CreateMap<CreateChannelCommandRequest, Domain.Entities.Channel>();
			CreateMap<Domain.Entities.Channel, CreateChannelCommandResponse>();

			CreateMap<ChannelDto, UpdateChannelCommandRequest>();
			CreateMap<UpdateChannelCommandRequest, Domain.Entities.Channel>();
			CreateMap<Domain.Entities.Channel, UpdateChannelCommandResponse>();


			CreateMap<Domain.Entities.Channel, GetChannelQueryResponse>();

			CreateMap<ChannelFilterModel, GetChannelsQueryRequest>();
			CreateMap<Domain.Entities.Channel, GetChannelsQueryResponse>();
			CreateMap<Domain.Entities.Channel, GetChannelQueryResponse>();
		}
	}
}
