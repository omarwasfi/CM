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

namespace CM.Library.Events.ProblemType
{
    public class AddProblemTypeCommandHandler : IRequestHandler<AddProblemTypeCommand, ProblemTypeDataModel>
    {
        private readonly EventsDBContext _eventsDBContext;
        private readonly CurrentStateDBContext _currentStateDBContext;

        public AddProblemTypeCommandHandler(EventsDBContext eventsDBContext, CurrentStateDBContext currentStateDBContext)
        {
            this._eventsDBContext = eventsDBContext;
            this._currentStateDBContext = currentStateDBContext;
        }
        public async Task<ProblemTypeDataModel> Handle(AddProblemTypeCommand request, CancellationToken cancellationToken)
        {
            EventDataModel addProblemTypeEvent = new EventDataModel();
            addProblemTypeEvent.Type = EventType.AddNewProblemType;
            addProblemTypeEvent.DateTime = DateTime.Now;
            addProblemTypeEvent.Content = JsonConvert.SerializeObject(request);

            await _eventsDBContext.Events.AddAsync(addProblemTypeEvent);
            await _eventsDBContext.SaveChangesAsync();

           return await applyEventToTheCurrentState(request);
         }
        private async Task<ProblemTypeDataModel> applyEventToTheCurrentState(AddProblemTypeCommand request)
        {
            ProblemTypeDataModel problemTypeDataModel = new ProblemTypeDataModel();

            problemTypeDataModel.Name = request.Name;

            await _currentStateDBContext.ProblemTypes.AddAsync(problemTypeDataModel);
            await _currentStateDBContext.SaveChangesAsync();

            return problemTypeDataModel;
        }
    }
}
