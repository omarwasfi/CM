using System;
using CM.Library.DataModels;
using CM.Library.DataModels.Chat;
using MediatR;

namespace CM.Library.Events.MessageContent
{
	public class SaveMessageContentCommand : IRequest<MessageContentDataModel>
	{
        public string Text { get; set; }

        public string PictureId { get; set; }

        public SaveMessageContentCommand(string text = null , string pictureId = null)
		{
			this.Text = text;
			this.PictureId = pictureId;
		}
	}
}

