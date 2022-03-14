using System;
using System.Collections.Generic;
using CM.Library.DataModels;
using CM.Library.DataModels.Chat;
using MediatR;

namespace CM.Library.Events.Room
{
	public class CreateRoomCommand : IRequest<RoomDataModel>
	{

        public List<PersonDataModel> People { get; set; }

        public CreateRoomCommand(List<PersonDataModel> people)
		{
			this.People = people;

		}
	}
}

