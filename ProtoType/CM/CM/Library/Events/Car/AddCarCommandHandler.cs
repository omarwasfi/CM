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

namespace CM.Library.Events.Car
{
    public class AddCarCommandHandler : IRequestHandler<AddCarCommand, CarDataModel>
    {
        private readonly EventsDBContext _eventsDBContext;
        private readonly CurrentStateDBContext _currentStateDBContext;

       
        public AddCarCommandHandler(EventsDBContext eventsDBContext, CurrentStateDBContext currentStateDBContext)
        {
            _eventsDBContext = eventsDBContext;
            _currentStateDBContext = currentStateDBContext;
        }

        public async Task<CarDataModel> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            EventDataModel addCarEventDataModel = new EventDataModel();
            addCarEventDataModel.Type = EventType.AddCar;
            addCarEventDataModel.DateTime = DateTime.Now;
            addCarEventDataModel.Content = JsonConvert.SerializeObject(request);

            await _eventsDBContext.Events.AddAsync(addCarEventDataModel);
            await _eventsDBContext.SaveChangesAsync();

            return await applyEventToTheCurrentState(request);
        }

        private async Task<CarDataModel> applyEventToTheCurrentState(AddCarCommand request)
        {
            CarDataModel carDataModel = new CarDataModel();

            carDataModel.Name = request.Name;
            carDataModel.Description = request.Description;
            carDataModel.CarBrand = await _currentStateDBContext.CarBrands.FindAsync(request.CarBrandId);

            await _currentStateDBContext.Cars.AddAsync(carDataModel);
            await _currentStateDBContext.SaveChangesAsync();

            return carDataModel;
        }

    }
}
