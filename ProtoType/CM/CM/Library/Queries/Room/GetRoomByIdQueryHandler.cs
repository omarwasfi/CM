using System;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels.Chat;
using CM.Library.DBContexts;
using MediatR;

namespace CM.Library.Queries.Room
{
	public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery,RoomDataModel>
	{
        private readonly CurrentStateDBContext _currentStateDBContext;


        public GetRoomByIdQueryHandler(CurrentStateDBContext currentStateDBContext)
		{
            this._currentStateDBContext = currentStateDBContext;
		}

        public async Task<RoomDataModel> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            return await _currentStateDBContext.Rooms.FindAsync(request.Id);
        }
    }
}

