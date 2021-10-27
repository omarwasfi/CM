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

namespace CM.Library.Events.CarBrand
{
    public class AddNewCarBrandCommandHandler : IRequestHandler<AddNewCarBrandCommand, CarBrandDataModel>
    {
        private readonly EventsDBContext _eventsDBContext;
        private readonly CurrentStateDBContext _currentStateDBContext;

        public AddNewCarBrandCommandHandler(EventsDBContext eventsDBContext, CurrentStateDBContext currentStateDBContext)
        {
            _eventsDBContext = eventsDBContext;
            _currentStateDBContext = currentStateDBContext;
        }

        public async Task<CarBrandDataModel> Handle(AddNewCarBrandCommand request, CancellationToken cancellationToken)
        {
            EventDataModel addCarBrandEventModel = new EventDataModel();
            addCarBrandEventModel.Type = EventType.AddCarBrand;
            addCarBrandEventModel.DateTime = DateTime.Now;
            addCarBrandEventModel.Content = JsonConvert.SerializeObject(request);

            await _eventsDBContext.Events.AddAsync(addCarBrandEventModel);
            await _eventsDBContext.SaveChangesAsync();
            
            return await applyEventToTheCurrentState(request);
        }

        private async Task<CarBrandDataModel> applyEventToTheCurrentState(AddNewCarBrandCommand request)
        {
            CarBrandDataModel carBrandDataModel = new CarBrandDataModel();

            carBrandDataModel.Name = request.Name;
            carBrandDataModel.Description = request.Description;

            await _currentStateDBContext.CarBrands.AddAsync(carBrandDataModel);
            await _currentStateDBContext.SaveChangesAsync();

            return carBrandDataModel;

        }
    }
}
