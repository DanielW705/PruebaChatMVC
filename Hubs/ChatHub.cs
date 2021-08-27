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
        /*********Lista de donde se guardan los usuarios conectados*************/
        static List<UserChat> listaUsuarios = new List<UserChat>();

        /***********Evento cuando se conecta el usuario***************/
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        /**************Evento de usuario conectado, para mandarlo a todos********************/
        public async Task UsuarioConectado(string nombre)
        {
            listaUsuarios.Add(new UserChat(Guid.NewGuid(), nombre, Context.ConnectionId));
            string lista = JsonSerializer.Serialize(listaUsuarios.ToArray());
            await Clients.All.SendAsync("RetornoDeConectados", lista);
        }
        /****************Desconexion de todos los usuarios********************/
        public async Task UsuarioDesconectado()
        {
            await Clients.All.SendAsync("RetornoDeDatos", listaUsuarios);
        }
        /**************Se borra al usuario de la lista para mantener actualizado***************/
        public override Task OnDisconnectedAsync(Exception exception)
        {
            UserChat usuarioAEliminar = (from usurio in listaUsuarios
                                         where usurio.idChat == Context.ConnectionId
                                         select usurio).First();
            listaUsuarios.Remove(usuarioAEliminar);
            return base.OnDisconnectedAsync(exception);
        }
        /***************Evento de Usuario envio mensaje****************/
        public async Task EnviarMensaje(string message, string IdReceiver)
        {
            //await Clients.All.SendAsync("MensajeRecibido", message);
            await Clients.Client(IdReceiver).SendAsync("MensajeRecibido", message);
        }

    }
}
