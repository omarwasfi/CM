using System;
using CM.Library.DataModels.Chat;
using MediatR;

namespace CM.Library.Queries.MessageContent
{
	public class GetMessageContentByIdQuery : IRequest<MessageContentDataModel>
	{
        public String Id { get; set; }
        public GetMessageContentByIdQuery(string id)
		{
			this.Id = id;

		}
	}
}

