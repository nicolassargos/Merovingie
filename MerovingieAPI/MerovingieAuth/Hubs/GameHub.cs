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


        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
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
