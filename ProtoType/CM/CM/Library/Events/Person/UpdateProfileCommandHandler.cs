using System;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels;
using CM.Library.DataModels.Events;
using CM.Library.DBContexts;
using CM.Library.Queries.Person;
using MediatR;
using Newtonsoft.Json;

namespace CM.Library.Events.Person
{
	public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
	{
        private CurrentStateDBContext _currentStateDBContext;
        private readonly EventsDBContext _eventsDBContext;
        private IMediator _mediator;


        public UpdateProfileCommandHandler(CurrentStateDBContext currentStateDBContext , EventsDBContext eventsDBContext, IMediator mediator)
		{
            this._currentStateDBContext = currentStateDBContext;
            this._eventsDBContext = eventsDBContext;
            this._mediator = mediator;
		}

        public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {

            EventDataModel logoutPersonEvent = new EventDataModel();

            logoutPersonEvent.Type = EventType.UpdateProfile;
            logoutPersonEvent.DateTime = DateTime.Now;
            logoutPersonEvent.Content = JsonConvert.SerializeObject(request);

            await _eventsDBContext.Events.AddAsync(logoutPersonEvent);
            await _eventsDBContext.SaveChangesAsync();


            PersonDataModel person = await _mediator.Send(new GetTheAuthorizedPersonQuery(request.ClaimsPrincipal));

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Gender = request.Gender;

            await _currentStateDBContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}

