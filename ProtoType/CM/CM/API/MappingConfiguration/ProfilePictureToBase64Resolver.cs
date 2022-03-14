using System;
using AutoMapper;
using CM.Library.DataModels;
using CM.Library.Queries.Picture;
using CM.SharedWithClient;
using MediatR;

namespace CM.API.MappingConfiguration
{
	public class ProfilePictureToBase64Resolver : IValueResolver<PersonDataModel,PersonDataViewModel, PictureBase64DataViewModel>
	{
        private IMediator _mediator { get; set; }

        public ProfilePictureToBase64Resolver(IMediator mediator)
		{

            this._mediator = mediator;
		}

        

        public PictureBase64DataViewModel Resolve(PersonDataModel source, PersonDataViewModel destination, PictureBase64DataViewModel destMember, ResolutionContext context)
        {
            PictureBase64DataViewModel pictureBase64DataViewModel = new PictureBase64DataViewModel();
            pictureBase64DataViewModel.Base64 = _mediator.Send(new GetPictureAsBase64Query(source.ProfilePicture)).Result;

            return pictureBase64DataViewModel;
        }
    }
}

