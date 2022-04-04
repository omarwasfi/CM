using System;
namespace CM.SharedWithClient
{
	public class MessageDataViewModel
	{
		public string? Id { get; set; }
		public string? RoomId { get; set; }

		public DateTime? DateTime { get; set; }

        public PersonDataViewModel? From { get; set; }

        public MessageContentDataViewModel? MessageContent { get; set; }

		public string? InitId { get; set; }

	}
}

