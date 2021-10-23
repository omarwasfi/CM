using CM.Library.DataModels;
using CM.Library.DataModels.Events;
using CM.Library.DBContexts;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<PersonDataModel> _userManager;
        public RegisterPersonHandler(EventsDBContext eventsDBContext, UserManager<PersonDataModel> userManager)
        {
            this._eventsDBContext = eventsDBContext;
            _userManager = userManager;
        }
        public async Task<PersonDataModel> Handle(RegisterPersonCommand request, CancellationToken cancellationToken)
        {
            EventDataModel registerPersonEvent = new EventDataModel();
            
            registerPersonEvent.Type = EventType.RegisterPerson;
            registerPersonEvent.DateTime = DateTime.Now;
            registerPersonEvent.Content = JsonConvert.SerializeObject(request);

            await _eventsDBContext.Events.AddAsync(registerPersonEvent);
            await _eventsDBContext.SaveChangesAsync();

            await applyEventToTheCurrentState(request);

            return request.PersonDataModel;
        }

        private async Task applyEventToTheCurrentState(RegisterPersonCommand request)
        {
            await _userManager.CreateAsync(request.PersonDataModel , request.Password);
           
        }
    }
}
