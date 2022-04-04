using System;
using System.Net;
using AutoMapper;
using CM.API.Hubs;
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
		public async Task<ActionResult<RoomDataViewModel>> StartPrivateChat(StartPrivateChatRequestDataViewModel startPrivateChatRequestDataViewModel)
		{
            try
            {
				RoomDataModel roomDataModel = await _mediator.Send(
					new StartPrivateChatCommand(startPrivateChatRequestDataViewModel.FirstPersonId,
					startPrivateChatRequestDataViewModel.SecondPersonId, this.User));

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
		public async Task<ActionResult> SubmitMessage(MessageRequestDataViewModel message)
		{
			try
			{
				MessageDataModel messageDataModel = await _mediator.Send(
					new SubmitMessageCommand(
						roomId: message.RoomId,
						fromPersonId: message.PersonId,
						text: message.MessageContentRequest.Text,
						pictureId: message.MessageContentRequest.pictureId,
						claimsPrincipal: this.User						
						)) ;

				MessageDataViewModel messageDataViewModel = _mapper.Map<MessageDataViewModel>(messageDataModel);
			

				List<string> userIdsInTheRoom = new List<string>();
				foreach(PersonDataModel person  in messageDataModel.Room.People)
                {
					userIdsInTheRoom.Add(person.Id);
                }



				await _hub.Clients.Users(userIdsInTheRoom).SendAsync("ReceiveMessage", messageDataViewModel);


				



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

