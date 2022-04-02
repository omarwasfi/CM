using System;
using System.Security.Claims;
using CM.Library.DataModels.Chat;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CM.Library.Events.Chat
{
	public class SubmitMessageCommand : IRequest<MessageDataModel>
	{
      

        public string RoomId { get; set; }

		public string FromPersonId { get; set; }

		public string Text { get; set; }

		public string PictureId { get; set; }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }



        public SubmitMessageCommand(string roomId, string fromPersonId, string text, string pictureId, ClaimsPrincipal claimsPrincipal)
        {
            RoomId = roomId;
            FromPersonId = fromPersonId;
            Text = text;
            PictureId = pictureId;
            ClaimsPrincipal = claimsPrincipal;
        }


    }
}

