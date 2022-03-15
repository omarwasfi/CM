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


        public UpdateProfileCommandHandler(CurrentStateDBContext currentStateDBContext, EventsDBContext eventsDBContext, IMediator mediator)
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


            PersonDataModel person = await _mediator.Send(new GetTheAuthorizedPersonQuery(request.ClaimsPrincipal));

            UpdateProfileCommandEventModel updateProfileCommandEventModel = new UpdateProfileCommandEventModel(request, person);

            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            logoutPersonEvent.Content = JsonConvert.SerializeObject(updateProfileCommandEventModel, settings);

            await _eventsDBContext.Events.AddAsync(logoutPersonEvent);
            await _eventsDBContext.SaveChangesAsync();


            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Gender = request.Gender;

            await _currentStateDBContext.SaveChangesAsync();

            return Unit.Value;
        }

        private class UpdateProfileCommandEventModel
        {
            public UpdateProfileCommand Request { get; private set; }

            public PersonDataModel Person { get; private set; }

            public UpdateProfileCommandEventModel(UpdateProfileCommand Request, PersonDataModel person)
            {
                this.Request = Request;
                this.Request.ClaimsPrincipal = null;
                this.Person = person;
            }
        } 
    }
}

