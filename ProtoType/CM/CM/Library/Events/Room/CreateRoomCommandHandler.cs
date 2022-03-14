using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels.Chat;
using CM.Library.DataModels.Events;
using CM.Library.DBContexts;
using MediatR;
using Newtonsoft.Json;

namespace CM.Library.Events.Room
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomDataModel>
	{
        private readonly EventsDBContext _eventsDBContext;
        private readonly CurrentStateDBContext _currentStateDBContext;

        public CreateRoomCommandHandler(EventsDBContext eventsDBContext, CurrentStateDBContext currentStateDBContext)
		{
            this._eventsDBContext = eventsDBContext;
            this._currentStateDBContext = currentStateDBContext;
        }

        public async Task<RoomDataModel> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            EventDataModel createRoomEvent = new EventDataModel();
            createRoomEvent.Type = EventType.CreateRoom;
            createRoomEvent.DateTime = DateTime.Now;
            
            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            createRoomEvent.Content = JsonConvert.SerializeObject(request, settings);

            await _eventsDBContext.Events.AddAsync(createRoomEvent);
            await _eventsDBContext.SaveChangesAsync();

            return await applyEventToTheCurrentState(request);
        }
        
        private async Task<RoomDataModel> applyEventToTheCurrentState(CreateRoomCommand request)
        {
            RoomDataModel roomDataModel = new RoomDataModel();
            roomDataModel.People = request.People;

            roomDataModel.Messages = new List<MessageDataModel>();

            await _currentStateDBContext.Rooms.AddAsync(roomDataModel);
            await _currentStateDBContext.SaveChangesAsync();

            return roomDataModel;
        }
    }
}

