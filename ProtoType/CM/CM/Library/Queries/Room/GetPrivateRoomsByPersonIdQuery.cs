using System;
using System.Collections.Generic;
using CM.Library.DataModels.Chat;
using MediatR;

namespace CM.Library.Queries.Room
{
	public class GetPrivateRoomsByPersonIdQuery : IRequest<List<RoomDataModel>>
	{
        public string PersonId { get; set; }
        public GetPrivateRoomsByPersonIdQuery(string personId)
		{
			this.PersonId = personId;
		}
	}
}

