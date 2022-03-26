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
	public class ChatHub : Hub
	{

		private IMediator _mediator { get; set; }
		private readonly IMapper _mapper;

		public ChatHub(IMediator mediator, IMapper mapper)
		{
			this._mediator = mediator;
			this._mapper = mapper;
		}

        public ChatHub()
        {

        }

		public async Task SendMessage( string message)
		{
			//ActionResult actionResult = await _chatController.SubmitMessage(message);
			//var user = this.Context.User;

			await Clients.All.SendAsync("ReceiveMessage", message);
		}

	}
}

