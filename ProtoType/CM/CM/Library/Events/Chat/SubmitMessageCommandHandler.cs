using System;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels.Chat;
using CM.Library.Events.Message;
using CM.Library.Events.MessageContent;
using MediatR;

namespace CM.Library.Events.Chat
{
	public class SubmitMessageCommandHandler : IRequestHandler<SubmitMessageCommand,MessageDataModel>
	{
        private readonly IMediator _mediator;

        public SubmitMessageCommandHandler(IMediator mediator)
		{
            this._mediator = mediator;
		}

        public async Task<MessageDataModel> Handle(SubmitMessageCommand request, CancellationToken cancellationToken)
        {
            MessageContentDataModel messageContent = await _mediator.Send(new SaveMessageContentCommand(request.Text,request.PictureId));

            return await _mediator.Send(new SaveMessageCommand(request.RoomId,request.FromPersonId,messageContent.Id,request.ClaimsPrincipal));
        }
    }
}

