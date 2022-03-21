using System;
using System.Collections.Generic;
using CM.Library.DataModels.Chat;
using MediatR;

namespace CM.Library.Queries.Room
{
	public class GetGroupRoomsByPersonIdQuery : IRequest<List<RoomDataModel>>
	{
        public string PersonId { get; set; }
        public GetGroupRoomsByPersonIdQuery(string personId)
		{
			this.PersonId = personId;
		}
	}
}

