using System;
using System.Net;
using AutoMapper;
using CM.Library.DataModels;
using CM.Library.DataModels.Chat;
using CM.Library.Events.Room;
using CM.Library.Queries.Person;
using CM.Library.Queries.Picture;
using CM.Library.Queries.Room;
using CM.SharedWithClient;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CM.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ChatController : ControllerBase
	{

		private IMediator _mediator { get; set; }
		private readonly IMapper _mapper;


		public ChatController(IMediator mediator, IMapper mapper)
		{
			this._mediator = mediator;
			this._mapper = mapper;
		}

		[HttpPost]
		[Route("CreateRoom")]
		public async Task<ActionResult<RoomDataViewModel>> CreateRoom(List<string> peopleId)
		{
			List<PersonDataModel> personDataModels = new List<PersonDataModel>();

			foreach(string personId in peopleId)
            {
				personDataModels.Add( await _mediator.Send(new GetPersonByIdQuery(personId)) );
            }

			RoomDataModel  roomDataModel = await _mediator.Send(new CreateRoomCommand(personDataModels));

			RoomDataViewModel roomDataViewModel = _mapper.Map<RoomDataViewModel>(roomDataModel);

			

			return Ok(roomDataViewModel);

		}

		[HttpPost]
		[Route("GetPrivateRoomsByPersonId")]
		public async Task<ActionResult<List<RoomDataViewModel>>> GetPrivateRoomsByPersonId(string personId)
        {
			List<RoomDataModel> privateRoomsDataModels = new List<RoomDataModel>();
			privateRoomsDataModels = await _mediator.Send(new GetPrivateRoomsByPersonIdQuery(personId));

			List<RoomDataViewModel> roomDataViewModels = _mapper.Map<List<RoomDataViewModel>>(privateRoomsDataModels);

			return roomDataViewModels;
        }



	}

}

