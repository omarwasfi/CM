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
        private IConfiguration _configuration;



        public PictureToBase64Resolver(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        public string Resolve(PictureDataModel source, PictureBase64DataViewModel destination, string destMember, ResolutionContext context)
        {
            return _mediator.Send(new GetPictureAsBase64Query(source, defaultProfilePictureFileName: _configuration.GetValue<string>("DefaultProfileImg:FileName"),
                    defaultProfilePictureFileExtension: _configuration.GetValue<string>("DefaultProfileImg:FileExtension"),
                    defaultProfilePictureFilePath: _configuration.GetValue<string>("DefaultProfileImg:Path"))).Result;
        }
    }
}

