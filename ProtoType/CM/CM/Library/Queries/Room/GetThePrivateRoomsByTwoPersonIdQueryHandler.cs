using System;
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
	public class GetThePrivateRoomsByTwoPersonIdQueryHandler : IRequestHandler<GetThePrivateRoomsByTwoPersonIdQuery, RoomDataModel>
	{
        private readonly CurrentStateDBContext _currentStateDBContext;
        private readonly IMediator _mediator;


        public GetThePrivateRoomsByTwoPersonIdQueryHandler(CurrentStateDBContext currentStateDBContext, IMediator mediator)
		{
            this._currentStateDBContext = currentStateDBContext;
            this._mediator = mediator;
        }

        public async Task<RoomDataModel> Handle(GetThePrivateRoomsByTwoPersonIdQuery request, CancellationToken cancellationToken)
        {
            RoomDataModel privateRoomDataModel = new RoomDataModel();

            PersonDataModel firstPersonDataModel = await _mediator.Send(new GetPersonByIdQuery(request.FirstPersonId));
            PersonDataModel secondPersonDataModel = await _mediator.Send(new GetPersonByIdQuery(request.SecondPersonId));

            try
            {
                privateRoomDataModel = _currentStateDBContext.Rooms.
                First(x => x.People.Contains(firstPersonDataModel) && x.People.Contains(secondPersonDataModel));

                privateRoomDataModel.Messages = privateRoomDataModel.Messages.OrderBy(x => x.DateTime).ToList();

                return privateRoomDataModel;
            }
            catch
            {
                return null;
            }
            
        }
    }
}

