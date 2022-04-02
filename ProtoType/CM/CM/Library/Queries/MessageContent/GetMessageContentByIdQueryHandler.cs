using System;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels.Chat;
using CM.Library.DBContexts;
using MediatR;

namespace CM.Library.Queries.MessageContent
{
	public class GetMessageContentByIdQueryHandler : IRequestHandler<GetMessageContentByIdQuery,MessageContentDataModel>
	{
        private readonly CurrentStateDBContext _currentStateDBContext;
        public GetMessageContentByIdQueryHandler(CurrentStateDBContext currentStateDBContext)
		{
            this._currentStateDBContext = currentStateDBContext;
		}

        public async Task<MessageContentDataModel> Handle(GetMessageContentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _currentStateDBContext.MessageContents.FindAsync(request.Id);
        }
    }
}

