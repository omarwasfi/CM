using System;
using CM.Library.DataModels;
using MediatR;

namespace CM.Library.Queries.Picture
{
	public class GetPictureByIdQuery : IRequest<PictureDataModel>
	{
        public string Id { get; set; }
        public GetPictureByIdQuery(string id)
		{
			this.Id = id;
		}
	}
}

