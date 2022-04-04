using System;
using CM.SharedWithClient;
using CM.SharedWithClient.RequestViewModels;
using CM.WebClient.Services.Interfaces;

namespace CM.WebClient.Services.Classes
{
	public class ChatService : IChatService
	{
        private IHttpService _httpService { get; set; }

        public ChatService(IHttpService httpService)
		{
            this._httpService = httpService;
		}

        public async Task SubmitMessage(MessageRequestDataViewModel message)
        {
            await _httpService.Post("Chat/SubmitMessage", message);

        }

        public async Task<RoomDataViewModel> StartPrivateChat(StartPrivateChatRequestDataViewModel startPrivateChatRequestDataViewModel)
        {
            return await _httpService.Post<RoomDataViewModel>("Chat/StartPrivateChat", startPrivateChatRequestDataViewModel);

        }
    }
}

