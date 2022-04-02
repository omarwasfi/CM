using System;
using System.Security.Claims;
using CM.Library.DataModels.Chat;
using MediatR;

namespace CM.Library.Events.Message
{
	public class SaveMessageCommand : IRequest<MessageDataModel>
	{
		public string RoomId { get; set; }

		public string FromPersonId { get; set; }

		public string MessageContentId { get; set; }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }


        public SaveMessageCommand(string roomId, string fromPersonId, string messageContentId, ClaimsPrincipal claimsPrincipal)
        {
            RoomId = roomId;
            FromPersonId = fromPersonId;
            MessageContentId = messageContentId;
            this.ClaimsPrincipal = claimsPrincipal;
        }
    }
}

