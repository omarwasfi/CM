using System;
namespace CM.SharedWithClient
{
	public class MessageDataViewModel
	{
		public string? Id { get; set; }

		public DateTime? DateTime { get; set; }

		public MessageContentDataViewModel? MessageContent { get; set; }


	}
}

