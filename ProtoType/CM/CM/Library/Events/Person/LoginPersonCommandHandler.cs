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
    public class LoginPersonCommandHandler : IRequestHandler<LoginPersonCommand>
    {
        private readonly EventsDBContext _eventsDBContext;

        private readonly UserManager<PersonDataModel> _userManager;
        private readonly SignInManager<PersonDataModel> _signInManager;
        public LoginPersonCommandHandler(EventsDBContext eventsDBContext, UserManager<PersonDataModel> userManager, SignInManager<PersonDataModel> signInManager)
        {
            this._eventsDBContext = eventsDBContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<Unit> Handle(LoginPersonCommand request, CancellationToken cancellationToken)
        {
            EventDataModel loginPersonEvent = new EventDataModel();

            loginPersonEvent.Type = EventType.LoginPerson;
            loginPersonEvent.DateTime = DateTime.Now;
            loginPersonEvent.Content = JsonConvert.SerializeObject(request);

            await _eventsDBContext.Events.AddAsync(loginPersonEvent);
            await _eventsDBContext.SaveChangesAsync();


            await applyEventToTheCurrentState(request);

            return Unit.Value;
        }
        private async Task applyEventToTheCurrentState(LoginPersonCommand request)
        {
            PersonDataModel user = await _userManager.FindByNameAsync(request.Usernamme);

            await _signInManager.SignInAsync(user, request.RememberMe);
        }
    }
}
