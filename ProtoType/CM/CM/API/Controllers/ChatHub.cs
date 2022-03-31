using System;
using System.Net;
using AutoMapper;
using CM.SharedWithClient.RequestViewModels;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CM.API.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ChatHub : Hub
	{
        public ChatHub()
        {

        }

		public async Task SendMessage(string message)
		{
			//ActionResult actionResult = await _chatController.SubmitMessage(message);
			//var user = this.Context.User;

			await Clients.All.SendAsync("ReceiveMessage", message);
		}

	}
}

