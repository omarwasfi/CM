using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels;
using CM.Library.DataModels.Chat;
using CM.Library.DBContexts;
using CM.Library.Queries.Person;
using MediatR;

namespace CM.Library.Queries.Room
{
	public class GetPrivateRoomsByPersonIdQueryHandler : IRequestHandler<GetPrivateRoomsByPersonIdQuery, List<RoomDataModel>>
	{
        private readonly CurrentStateDBContext _currentStateDBContext;
        private readonly IMediator _mediator;


        public GetPrivateRoomsByPersonIdQueryHandler(CurrentStateDBContext currentStateDBContext, IMediator mediator)
		{
            this._currentStateDBContext = currentStateDBContext;
            this._mediator = mediator;
		}

        public async Task<List<RoomDataModel>> Handle(GetPrivateRoomsByPersonIdQuery request, CancellationToken cancellationToken)
        {
            PersonDataModel personDataModel = new PersonDataModel();
            personDataModel = await _mediator.Send(new GetPersonByIdQuery(request.PersonId));

            List<RoomDataModel> privateRooms = new List<RoomDataModel>();

             privateRooms = _currentStateDBContext.Rooms.
                Where(x => x.People.Contains(personDataModel) && x.People.Count == 2).ToList();
            return privateRooms;
        }
    }
}

