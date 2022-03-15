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

			CreateMap<PersonDataModel, PersonDataViewModel>()
				.ForMember(p => p.ProfilePicture, opt => opt.MapFrom<ProfilePictureToBase64Resolver>());
			CreateMap<RoomDataModel, RoomDataViewModel>();
		}

		
	}

    
}

