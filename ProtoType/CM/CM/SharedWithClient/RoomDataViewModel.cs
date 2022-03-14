using System;
namespace CM.SharedWithClient
{
	public class RoomDataViewModel
	{
		public string? Id { get; set; }

		public  List<MessageDataViewModel>? Messages { get; set; }

		public List<PersonDataViewModel>? People { get; set; }
	}
}

