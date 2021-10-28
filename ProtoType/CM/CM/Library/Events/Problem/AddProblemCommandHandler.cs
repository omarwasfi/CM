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

namespace CM.Library.Events.Problem
{
    public class AddProblemCommandHandler : IRequestHandler<AddProblemCommand, ProblemDataModel>
    {
        private readonly EventsDBContext _eventsDBContext;

        private readonly CurrentStateDBContext _currentStateDBContext;

        public AddProblemCommandHandler(EventsDBContext eventsDBContext, CurrentStateDBContext currentStateDBContext)
        {
            _eventsDBContext = eventsDBContext;
            _currentStateDBContext = currentStateDBContext;
        }

        public async Task<ProblemDataModel> Handle(AddProblemCommand request, CancellationToken cancellationToken)
        {
            EventDataModel addProblemEvent = new EventDataModel();
            addProblemEvent.Type = EventType.AddNewProblem;
            addProblemEvent.DateTime = DateTime.Now;
            addProblemEvent.Content = JsonConvert.SerializeObject(request);

            return await applyEventToTheCurrentState(request);

        }

        private async Task<ProblemDataModel> applyEventToTheCurrentState(AddProblemCommand request)
        {
            ProblemDataModel problemDataModel = new ProblemDataModel();
            problemDataModel.ProblemType = await _currentStateDBContext.ProblemTypes.FindAsync(request.ProblemTypeId);
            problemDataModel.Person = await _currentStateDBContext.Users.FindAsync(request.PersonId);
            problemDataModel.OwnedCar = await _currentStateDBContext.OwnedCars.FindAsync(request.OwnedCarId);
            problemDataModel.State = ProblemState.OnHold;
            problemDataModel.Description = request.Description;
            problemDataModel.DateTime = DateTime.Now;
            problemDataModel.Location = request.Location;


            await _currentStateDBContext.Problems.AddAsync(problemDataModel);
            await _currentStateDBContext.SaveChangesAsync();

            return problemDataModel;
        }

    }
}
