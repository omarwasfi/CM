﻿using CM.Library.DataModels;
using CM.Library.DataModels.Events;
using CM.Library.DBContexts;
using CM.Library.Events.Token;
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
    public class RegisterPersonHandler : IRequestHandler<RegisterPersonCommand, string>
    {
        private readonly EventsDBContext _eventsDBContext;
        private readonly UserManager<PersonDataModel> _userManager;
        private readonly IMediator _mediator;

        public RegisterPersonHandler(IMediator mediator, EventsDBContext eventsDBContext, UserManager<PersonDataModel> userManager)
        {
            this._eventsDBContext = eventsDBContext;
            this._mediator = mediator;
            _userManager = userManager;
        }
        public async Task<string> Handle( RegisterPersonCommand request, CancellationToken cancellationToken)
        {
            EventDataModel registerPersonEvent = new EventDataModel();
            
            registerPersonEvent.Type = EventType.RegisterPerson;
            registerPersonEvent.DateTime = DateTime.Now;
            registerPersonEvent.Content = JsonConvert.SerializeObject(request);

            await _eventsDBContext.Events.AddAsync(registerPersonEvent);
            await _eventsDBContext.SaveChangesAsync();

            await applyEventToTheCurrentState(request);

            return await _mediator.Send(new CreateTokenByUserNameCommand(request.UserName,request.Issuer,request.Audience));
        }

        private async Task applyEventToTheCurrentState(RegisterPersonCommand request)
        {
            PersonDataModel person = new PersonDataModel()
            {
                UserName = request.UserName

            };
            await _userManager.CreateAsync(person, request.Password);
           
        }
    }
}
