using System;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels.Chat;
using CM.Library.DBContexts;
using CM.Library.Queries.MessageContent;
using CM.Library.Queries.Person;
using CM.Library.Queries.Room;
using MediatR;

namespace CM.Library.Events.Message
{
	public class SaveMessageCommandHandler : IRequestHandler<SaveMessageCommand , MessageDataModel>
	{
         private readonly CurrentStateDBContext _currentStateDBContext;
        private readonly IMediator _mediator;


        public SaveMessageCommandHandler(CurrentStateDBContext currentStateDBContext , IMediator mediator)
		{
            this._currentStateDBContext = currentStateDBContext;
            this._mediator = mediator;

		}

        public async Task<MessageDataModel> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
        {
            MessageDataModel messageDataModel = new MessageDataModel();
            messageDataModel.From = await _mediator.Send(new GetPersonByIdQuery(request.FromPersonId));
            messageDataModel.Room = await _mediator.Send(new GetRoomByIdQuery(request.RoomId));
            messageDataModel.MessageContent = await _mediator.Send(new GetMessageContentByIdQuery(request.MessageContentId));
            messageDataModel.DateTime = DateTime.UtcNow;

            await _currentStateDBContext.Messages.AddAsync(messageDataModel);

            await _currentStateDBContext.SaveChangesAsync();

            return messageDataModel;

        }
    }
}

