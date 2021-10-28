using CM.Library.DataModels.BusinessModels;
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

namespace CM.Library.Events.OwnedCar
{
    public class AddOwnedCarCommandHandler : IRequestHandler<AddOwnedCarCommand, OwnedCarDataModel>
    {
        private readonly EventsDBContext _eventsDBContext;
        private readonly CurrentStateDBContext _currentStateDBContext;
        private readonly IMediator _mediator;


        public AddOwnedCarCommandHandler(EventsDBContext eventsDBContext, CurrentStateDBContext currentStateDBContext, IMediator mediator)
        {
            this._eventsDBContext = eventsDBContext;
            _currentStateDBContext = currentStateDBContext;
            _mediator = mediator;

        }

        public async Task<OwnedCarDataModel> Handle(AddOwnedCarCommand request, CancellationToken cancellationToken)
        {
            EventDataModel addOwnedCarEvent = new EventDataModel();
            addOwnedCarEvent.Type = EventType.AddOwnedCar;
            addOwnedCarEvent.DateTime = DateTime.Now;
            addOwnedCarEvent.Content = JsonConvert.SerializeObject(request);

            await _eventsDBContext.Events.AddAsync(addOwnedCarEvent);
            await _eventsDBContext.SaveChangesAsync();

            return await applyEventToTheCurrentState(request);
        }

        private async Task<OwnedCarDataModel> applyEventToTheCurrentState(AddOwnedCarCommand request)
        {
            OwnedCarDataModel ownedCarDataModel = new OwnedCarDataModel();

            ownedCarDataModel.Name = request.Name;
            ownedCarDataModel.Description = request.Description;
            ownedCarDataModel.Person = await _currentStateDBContext.Users.FindAsync(request.PersonId);
            ownedCarDataModel.Car = await _currentStateDBContext.Cars.FindAsync(request.CarId);

            await _currentStateDBContext.OwnedCars.AddAsync(ownedCarDataModel);
            await _currentStateDBContext.SaveChangesAsync();

            return ownedCarDataModel;
        }
    }
}
