using CM.Library.DataModels;
using CM.Library.DataModels.Events;
using CM.Library.Events.Token;
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
using System.IdentityModel.Tokens.Jwt;

namespace CM.Library.Events.Person
{
    public class LoginPersonCommandHandler : IRequestHandler<LoginPersonCommand, string>
    {
        private readonly EventsDBContext _eventsDBContext;

        private readonly UserManager<PersonDataModel> _userManager;
        private readonly SignInManager<PersonDataModel> _signInManager;

        private readonly IMediator _mediator;
        public LoginPersonCommandHandler(IMediator mediator,EventsDBContext eventsDBContext, UserManager<PersonDataModel> userManager, SignInManager<PersonDataModel> signInManager)
        {
            this._eventsDBContext = eventsDBContext;
            _userManager = userManager;
            _signInManager = signInManager;
            this._mediator = mediator;
        }
        public async Task<string> Handle(LoginPersonCommand request, CancellationToken cancellationToken)
        {
            EventDataModel loginPersonEvent = new EventDataModel();

            loginPersonEvent.Type = EventType.LoginPerson;
            loginPersonEvent.DateTime = DateTime.Now;

            LoginPersonCommand tempLoginPersonCommand = request;
            tempLoginPersonCommand.Password = "Not Saved Here";

            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            loginPersonEvent.Content = JsonConvert.SerializeObject(tempLoginPersonCommand, settings);

            await _eventsDBContext.Events.AddAsync(loginPersonEvent);
            await _eventsDBContext.SaveChangesAsync();


            await applyEventToTheCurrentState(request);



            return await _mediator.Send(new CreateTokenByUserNameCommand(request.Usernamme,request.Issuer,request.Audience,request.JwtKey));
        }
        private async Task applyEventToTheCurrentState(LoginPersonCommand request)
        {
            PersonDataModel user = await _userManager.FindByNameAsync(request.Usernamme);

            await _signInManager.SignInAsync(user, request.RememberMe);
        }
    }
}
