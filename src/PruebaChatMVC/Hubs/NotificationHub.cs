using LibreriaChatMVC.Entities;
using LibreriaChatMVC.Hubs;
using Microsoft.AspNetCore.SignalR;
using PruebaChatMVC.Models;

namespace PruebaChatMVC.Hubs
{
    public class NotificationHub : Hub<INotificationsClient>
    {
        private static List<ClientHubInformation> _clientsHubs = new();
        public override async Task OnConnectedAsync()
        {
            var information = _clientsHubs.First(ch => ch.ContextId is not null && ch.ContextId.Equals(Context.ConnectionId));
            information.isConnected = true;
            await Clients.All.OnNewUserConnect(information.UserId);
        }

    }
}
