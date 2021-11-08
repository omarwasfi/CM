using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace CM.Server.Services.Chat
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string fromUser, string toUser, string message)
        {
            await Clients.Users(fromUser,toUser).SendAsync("ReceiveMessage",fromUser, message);
        }

    }
}
