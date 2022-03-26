using System;
namespace CM.SharedWithClient
{
	public class MessageDataViewModel
	{
		public string? Id { get; set; }

		public DateTime? DateTime { get; set; }

        public PersonDataViewModel? From { get; set; }

        public RoomDataViewModel? Room { get; set; }

        public MessageContentDataViewModel? MessageContent { get; set; }

		public string? InitId { get; set; }

	}
}

