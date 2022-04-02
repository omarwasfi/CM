using System;
using AutoMapper;
using CM.Library.DataModels;
using CM.Library.DataModels.Chat;
using CM.Library.Queries.Picture;
using CM.SharedWithClient;
using MediatR;

namespace CM.API.MappingConfiguration
{
	public class AutoMapperProfile : Profile
	{

		public AutoMapperProfile()
		{

			CreateMap<PersonDataModel, PersonDataViewModel>();
			CreateMap<PictureDataModel, PictureBase64DataViewModel>()
				.ForMember(p => p.Base64 ,opt => opt.MapFrom<PictureToBase64Resolver>());

			CreateMap<RoomDataModel, RoomDataViewModel>();

			CreateMap<MessageDataModel, MessageDataViewModel>();

			CreateMap<MessageContentDataModel, MessageContentDataViewModel>();


		}

		
	}

    
}

