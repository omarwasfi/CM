using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CM.API.Hubs
{
	
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public class ChatHub : Hub
		{
			public ChatHub()
			{

			}

			


		}
	
}

