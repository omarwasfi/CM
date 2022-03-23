using System;
using System.Security.Claims;
using CM.Library.DataModels.Chat;
using MediatR;

namespace CM.Library.Events.Chat
{
	public class StartPrivateChatCommand : IRequest<RoomDataModel>
	{
        public string FirstPersonId { get; set; }

        public string SecondPersonId { get; set; }

		public ClaimsPrincipal ClaimsPrincipal { get; set; }

		public StartPrivateChatCommand(string firstPersonId , string secondPersonId, ClaimsPrincipal claimsPrincipal)
		{
			this.FirstPersonId = firstPersonId;
			this.SecondPersonId = secondPersonId;
			this.ClaimsPrincipal = claimsPrincipal;
		}
	}
}

