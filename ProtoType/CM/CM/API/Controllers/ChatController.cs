using System;
using System.Net;
using AutoMapper;
using CM.Library.DataModels;
using CM.Library.DataModels.Chat;
using CM.Library.Events.Chat;
using CM.Library.Events.Room;
using CM.Library.Queries.Person;
using CM.Library.Queries.Picture;
using CM.Library.Queries.Room;
using CM.SharedWithClient;
using CM.SharedWithClient.RequestViewModels;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CM.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ChatController : ControllerBase
	{

		private IMediator _mediator { get; set; }
		private readonly IMapper _mapper;

		private readonly IHubContext<ChatHub> _hub;



		public ChatController(IMediator mediator, IMapper mapper, IHubContext<ChatHub> hub)
		{
			this._mediator = mediator;
			this._mapper = mapper;
			this._hub = hub;
		}


		[HttpPost]
		[Route("StartPrivateChat")]
		public async Task<ActionResult<RoomDataViewModel>> StartPrivateChat(string firstPersonId , string secondPersonId)
		{
            try
            {
				RoomDataModel roomDataModel = await _mediator.Send(new StartPrivateChatCommand(firstPersonId, secondPersonId, this.User));

				RoomDataViewModel roomDataViewModel = _mapper.Map<RoomDataViewModel>(roomDataModel);

				return Ok(roomDataViewModel);
			}
			catch (ValidationException v)
			{
				return ValidationProblem(v.Message);
			}
			catch
			{
				return BadRequest("Unrecognized error");

			}

		}

		[HttpGet]
		[Route("GetPrivateRoomsByPersonId")]
		public async Task<ActionResult<List<RoomDataViewModel>>> GetPrivateRoomsByPersonId(string personId)
        {
			try
			{
				List<RoomDataModel> privateRoomsDataModels = new List<RoomDataModel>();
				privateRoomsDataModels = await _mediator.Send(new GetPrivateRoomsByPersonIdQuery(personId));

				List<RoomDataViewModel> roomDataViewModels = _mapper.Map<List<RoomDataViewModel>>(privateRoomsDataModels);

				return roomDataViewModels;
			}
			catch (ValidationException v)
			{
				return ValidationProblem(v.Message);
			}
			catch
			{
				return BadRequest("Unrecognized error");

			}
		}

		[HttpGet]
		[Route("GetGroupRoomsByPersonId")]
		public async Task<ActionResult<List<RoomDataViewModel>>> GetGroupRoomsByPersonId(string personId)
		{
            try
            {
				List<RoomDataModel> groupRoomsDataModels = new List<RoomDataModel>();
				groupRoomsDataModels = await _mediator.Send(new GetGroupRoomsByPersonIdQuery(personId));

				List<RoomDataViewModel> roomDataViewModels = _mapper.Map<List<RoomDataViewModel>>(groupRoomsDataModels);

				return roomDataViewModels;
			}
			catch (ValidationException v)
			{
				return ValidationProblem(v.Message);
			}
			catch
			{
				return BadRequest("Unrecognized error");

			}

			
		}


		[HttpPost]
		[Route("SubmitMessage")]
		public async Task<ActionResult> SubmitMessage(string message)
		{
			try
			{
				//var user = this.User;

				await _hub.Clients.User("dcdcb4c1-a29f-4c5c-b6cd-698b510e1c75").SendAsync("ReceiveMessage", message);

				await _hub.Clients.User("88711814-c358-467c-956c-164be68b9929").SendAsync("ReceiveMessage", message);



				return Ok();
			}
			catch (ValidationException v)
			{
				return ValidationProblem(v.Message);
			}
			catch
			{
				return BadRequest("Unrecognized error");

			}

		}



	}

}

