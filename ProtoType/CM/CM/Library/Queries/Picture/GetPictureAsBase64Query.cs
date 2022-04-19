using System;
using CM.Library.DataModels;
using MediatR;

namespace CM.Library.Queries.Picture
{
	public class GetPictureAsBase64Query : IRequest<string>
	{
        public PictureDataModel Picture { get; set; }

		public string DefaultProfilePictureFileName { get; set; }

		public string DefaultProfilePictureFileExtension { get; set; }

		public string DefaultProfilePictureFilePath { get; set; }


        public GetPictureAsBase64Query(PictureDataModel picture, string defaultProfilePictureFileName, string defaultProfilePictureFileExtension, string defaultProfilePictureFilePath)
        {
            Picture = picture;
            DefaultProfilePictureFileName = defaultProfilePictureFileName;
            DefaultProfilePictureFileExtension = defaultProfilePictureFileExtension;
            DefaultProfilePictureFilePath = defaultProfilePictureFilePath;
        }
    }
}

