using System;
namespace CM.SharedWithClient.RequestViewModels
{
	public class MessageRequestDataViewModel
	{
        public string? RoomId { get; set; }

        public string? PersonId { get; set; }

        public MessageContentRequestDataViewModel? MessageContentRequest { get; set; }

		/// <summary>
        /// Will be generated on the frontend side
        /// </summary>
		public string? InitId { get; set; }
	}
}

