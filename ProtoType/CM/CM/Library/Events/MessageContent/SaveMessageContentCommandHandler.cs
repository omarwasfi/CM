using System;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels.Chat;
using CM.Library.DBContexts;
using CM.Library.Queries.Picture;
using MediatR;

namespace CM.Library.Events.MessageContent
{
    public class SaveMessageContentCommandHandler : IRequestHandler<SaveMessageContentCommand, MessageContentDataModel>
	{
        private readonly CurrentStateDBContext _currentStateDBContext;
        private readonly IMediator _mediator;



        public SaveMessageContentCommandHandler(CurrentStateDBContext currentStateDBContext , IMediator mediator)
		{
            this._currentStateDBContext = currentStateDBContext;

            this._mediator = mediator;
		}

        public async Task<MessageContentDataModel> Handle(SaveMessageContentCommand request, CancellationToken cancellationToken)
        {
            MessageContentDataModel messageContentDataModel = new MessageContentDataModel();


            if (String.IsNullOrEmpty(request.Text) == false && String.IsNullOrEmpty(request.PictureId) == true)
            {
                messageContentDataModel.Text = request.Text;
            }

            if (String.IsNullOrEmpty(request.Text) == true && String.IsNullOrEmpty(request.PictureId) == false)
            {
                messageContentDataModel.Picture = await _mediator.Send(new GetPictureByIdQuery(request.PictureId));
            }

            await _currentStateDBContext.MessageContents.AddAsync(messageContentDataModel);
            await _currentStateDBContext.SaveChangesAsync();

            return messageContentDataModel;
        }
    }
}

