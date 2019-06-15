using AoC.Map;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.WebSockets.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerovingieAuth.Hubs
{
    public class GameHub : Hub
    {
        
        List<ChatMessage> _conversations = new List<ChatMessage>();

        public async Task SendMessage(string user, string userColor, string message)
        {
            _conversations.Add(new ChatMessage { UserName = user, UserColor = userColor, Message = message });
            await Clients.All.SendAsync("ReceiveMessage", user, userColor, message);
        }

        public async Task GetConversations()
        {
            await Clients.Caller.SendAsync("DisplayConversation", _conversations);
        }

        /// <summary>
        ///  Fires when a player is connected to hub.
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            Clients.All.SendAsync("Player is connected");
            return base.OnConnectedAsync();
        }
    }
}
