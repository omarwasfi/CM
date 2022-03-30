using System;
using AutoMapper;
using CM.Library.DataModels;
using CM.Library.Queries.Picture;
using CM.SharedWithClient;
using MediatR;

namespace CM.API.MappingConfiguration
{
	public class PictureToBase64Resolver : IValueResolver<PictureDataModel,PictureBase64DataViewModel, string>
	{
        private IMediator _mediator { get; set; }

        public PictureToBase64Resolver(IMediator mediator)
		{

            this._mediator = mediator;
		}

        
        public string Resolve(PictureDataModel source, PictureBase64DataViewModel destination, string destMember, ResolutionContext context)
        {
            return _mediator.Send(new GetPictureAsBase64Query(source)).Result;
        }
    }
}

