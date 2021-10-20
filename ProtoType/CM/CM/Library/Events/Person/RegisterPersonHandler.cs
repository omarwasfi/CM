using CM.Library.DataModels;
using CM.Library.DBContexts;
using MediatR;
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
            
            throw new NotImplementedException();
        }
    }
}
