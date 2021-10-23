using CM.Library.DataModels;
using CM.Library.DataModels.Events;
using CM.Library.DBContexts;
using CM.Library.Queries.Person;
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
    public class LogoutPersonHandler : IRequestHandler<LogoutPersonCommand>
    {
        private readonly SignInManager<PersonDataModel> _signInManager;

        private EventsDBContext _eventsDBContext;
        public LogoutPersonHandler(SignInManager<PersonDataModel> signInManager, EventsDBContext eventsDBContext)
        {
            _signInManager = signInManager;
            _eventsDBContext = eventsDBContext;
        }
        public async Task<Unit> Handle(LogoutPersonCommand request, CancellationToken cancellationToken)
        {
            EventDataModel logoutPersonEvent = new EventDataModel();

            logoutPersonEvent.Type = EventType.LogoutPerson;
            logoutPersonEvent.DateTime = DateTime.Now;
            logoutPersonEvent.Content = JsonConvert.SerializeObject(request);
                
            await _eventsDBContext.Events.AddAsync(logoutPersonEvent);
            await _eventsDBContext.SaveChangesAsync();

            await _signInManager.SignOutAsync();
            return Unit.Value;
        }
    }
}
