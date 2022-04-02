using System;
using MediatR;

namespace CM.Library.Queries.Picture
{
	public class IsPictureIdExistQuery : IRequest<bool>
	{
        public string Id { get; set; }
        public IsPictureIdExistQuery(string id)
		{
			this.Id = id;
		}
	}
}

