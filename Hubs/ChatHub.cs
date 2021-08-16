using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace PruebaChatMVC.Hubs
{
    public class ChatHub : Hub
    {
        static List<string> IDConectados = new List<string>();
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("MessageRecive", user, message);
        }
        public override Task OnConnectedAsync()
        {
            IDConectados.Add(Context.ConnectionId);
            Clients.All.SendAsync("UserConnect", IDConectados.ToArray());
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            IDConectados.Remove(Context.ConnectionId);
            Clients.All.SendAsync("UserDisconect", IDConectados.ToArray());
            return base.OnDisconnectedAsync(exception);
        }
    }
}
