using System;
using CM.Library.DataModels.Chat;
using MediatR;

namespace CM.Library.Queries.Room
{
	public class GetRoomByIdQuery : IRequest<RoomDataModel>
	{
        public string Id { get; set; }
        public GetRoomByIdQuery(string id)
		{
			this.Id = id;

		}
	}
}

