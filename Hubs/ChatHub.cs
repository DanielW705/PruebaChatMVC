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
        private readonly Model DBModel;
        /*********Lista de donde se guardan los usuarios conectados*************/
        static List<UserChat> listaUsuarios = new List<UserChat>();
        /***********Evento cuando se conecta el usuario***************/
        public ChatHub(Model _DBModel)
        {
            DBModel = _DBModel;
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        /**************Evento de usuario conectado, para mandarlo a todos********************/
        public async Task UsuarioConectado(string id, string nombre)
        {
            listaUsuarios.Add(new UserChat(Guid.Parse(id), nombre, Context.ConnectionId));
            string lista = JsonSerializer.Serialize(listaUsuarios.ToArray());
            DBModel.UsuarioChat.Add(new UserChat { idUser = Guid.Parse(id), idChat = Context.ConnectionId, UserName = nombre });
            DBModel.SaveChanges();
            await Clients.All.SendAsync("RetornoDeConectados", lista);
        }
        /****************Desconexion de todos los usuarios********************/
        public async Task UsuarioDesconectado()
        {
            await Clients.All.SendAsync("RetornoDeDatos", listaUsuarios);
        }
        /**************Se borra al usuario de la lista para mantener actualizado***************/
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            UserChat usuarioAEliminar = (from usurio in listaUsuarios
                                         where usurio.idChat == Context.ConnectionId
                                         select usurio).First();
            string lista = JsonSerializer.Serialize(listaUsuarios.ToArray());
            DBModel.UsuarioChat.Remove(usuarioAEliminar);
            DBModel.SaveChanges();
            listaUsuarios.Remove(usuarioAEliminar);
            await Clients.All.SendAsync("RetornoDeConectados", lista);
            await base.OnDisconnectedAsync(exception);
        }
        /***************Evento de Usuario envio mensaje****************/
        public async Task EnviarMensaje(string message, string IdReceiver, string date)
        {
            string idMio = Context.ConnectionId;
            string Sender = (from usuario in listaUsuarios
                             where usuario.idChat == Context.ConnectionId
                             select usuario.UserName).First();
            Guid idSender = (from usuario in listaUsuarios
                             where usuario.idChat == idMio
                             select usuario.idUser).First();
            Guid idReciber = (from usuario in listaUsuarios
                              where usuario.idChat == IdReceiver
                              select usuario.idUser).First();
            DBModel.MensajesEnviados.Add(new MessageSended
            {
                idMensaje = Guid.NewGuid(),
                nameSender = Sender,
                Mensaje = message,
                FechaDeEnvio = DateTime.Parse(date),
                Sender = idSender,
                Reciber = idReciber,
                visto = false
            });
            DBModel.SaveChanges();
            await Clients.Client(idMio).SendAsync("MensajeMio", message, date, Sender);
            await Clients.Client(IdReceiver).SendAsync("MensajeRecibido", message, date, Sender, idMio);
        }

    }
}
