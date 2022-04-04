using System;
using CM.SharedWithClient;
using CM.SharedWithClient.RequestViewModels;

namespace CM.WebClient.Services.Interfaces
{
	public interface IChatService
	{
		public Task SubmitMessage(MessageRequestDataViewModel message);

		public Task<RoomDataViewModel> StartPrivateChat(StartPrivateChatRequestDataViewModel startPrivateChatRequestDataViewModel);

	}
}

