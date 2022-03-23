using System;
using System.Security.Claims;
using CM.Library.DataModels.Chat;
using MediatR;

namespace CM.Library.Queries.Room
{
	public class GetThePrivateRoomsByTwoPersonIdQuery : IRequest<RoomDataModel>
	{
		public string FirstPersonId { get; set; }

		public string SecondPersonId { get; set; }


		public GetThePrivateRoomsByTwoPersonIdQuery(string firstPersonId ,string secondPersonId)
		{
			this.FirstPersonId = firstPersonId;
			this.SecondPersonId = secondPersonId;
		}
	}
}

