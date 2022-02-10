using System;
using CM.Library.DataModels;
using MediatR;

namespace CM.Library.Queries.Picture
{
	public class GetPictureAsBase64Query : IRequest<string>
	{
        public PictureDataModel Picture { get; set; }

        public GetPictureAsBase64Query(PictureDataModel pictureDataModel)
		{
			this.Picture = pictureDataModel;
		}
	}
}

