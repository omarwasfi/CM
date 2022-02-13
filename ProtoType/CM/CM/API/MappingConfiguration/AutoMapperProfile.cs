using System;
using AutoMapper;
using CM.Library.DataModels;
using CM.SharedWithClient;

namespace CM.API.MappingConfiguration
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<PersonDataModel, PersonDataViewModel>()
				.ForMember(x=>x.ProfilePicture,opt=>opt.Ignore());
		}
	}
}

