using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Text.Json;
using PruebaChatMVC.Models;

namespace PruebaChatMVC.Hubs
{
    public class ChatHub : Hub
    {
        static List<UserChat> listaUsuarios = new List<UserChat>();
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public async Task UsuarioConectado(string nombre)
        {
            listaUsuarios.Add(new UserChat(Guid.NewGuid(), nombre, Context.ConnectionId));
            string lista = JsonSerializer.Serialize(listaUsuarios.ToArray());
            await Clients.All.SendAsync("RetornoDeConectados", lista);
        }
        public async Task UsuarioDesconectado()
        {
            await Clients.All.SendAsync("RetornoDeDatos", listaUsuarios);
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            UserChat usuarioAEliminar = (from usurio in listaUsuarios
                                         where usurio.idChat == Context.ConnectionId
                                         select usurio).First();
            listaUsuarios.Remove(usuarioAEliminar);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
