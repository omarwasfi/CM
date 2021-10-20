using CM.Library.DataModels;
using CM.Library.DataModels.Events;
using CM.Library.DBContexts;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Library.Events.Person
{
    public class RegisterPersonHandler : IRequestHandler<RegisterPersonCommand, PersonDataModel>
    {
        private readonly EventsDBContext _eventsDBContext;
        public RegisterPersonHandler(EventsDBContext eventsDBContext)
        {
            this._eventsDBContext = eventsDBContext;
        }
        public async Task<PersonDataModel> Handle(RegisterPersonCommand request, CancellationToken cancellationToken)
        {
            EventDataModel registerPersonEvent = new EventDataModel();
            
            registerPersonEvent.Type = EventType.RegisterPerson;
            registerPersonEvent.DateTime = DateTime.Now;
            registerPersonEvent.Content = JsonConvert.SerializeObject(request.PersonDataModel);

            await _eventsDBContext.Events.AddAsync(registerPersonEvent);
            await _eventsDBContext.SaveChangesAsync();

            // TODO - Triger some other method to run the events

            return request.PersonDataModel;
        }
    }
}
