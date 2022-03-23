using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels;
using CM.Library.DataModels.Chat;
using CM.Library.Events.Room;
using CM.Library.Queries.Person;
using CM.Library.Queries.Room;
using MediatR;

namespace CM.Library.Events.Chat
{
	public class StartPrivateChatCommandHandler : IRequestHandler<StartPrivateChatCommand , RoomDataModel>
	{
        private readonly IMediator _mediator;

        public StartPrivateChatCommandHandler(IMediator mediator)
		{
            this._mediator = mediator;
		}

        public async Task<RoomDataModel> Handle(StartPrivateChatCommand request, CancellationToken cancellationToken)
        {
            RoomDataModel privateRoom = new RoomDataModel();
            privateRoom = await _mediator.Send(new GetThePrivateRoomsByTwoPersonIdQuery(request.FirstPersonId, request.SecondPersonId));

            if(privateRoom!= null)
            {
                return privateRoom;
            }
            else
            {
                List<PersonDataModel> personDataModels = new List<PersonDataModel>();
                personDataModels.Add( await _mediator.Send(new GetPersonByIdQuery(request.FirstPersonId)));
                personDataModels.Add(await _mediator.Send(new GetPersonByIdQuery(request.SecondPersonId)));

                privateRoom = await _mediator.Send(new CreateRoomCommand(personDataModels));

                return privateRoom;
            }

        }
    }
}

