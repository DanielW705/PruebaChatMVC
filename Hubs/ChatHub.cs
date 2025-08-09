using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using PruebaChatMVC.Models;
using PruebaChatMVC.Dto;
using PruebaChatMVC.UseCase;
using ROP;

namespace PruebaChatMVC.Hubs
{
    public class ChatHub : Hub
    {
        private readonly HandlerMessagesUseCase _handlerMessagesUseCase;
        /*********Lista de donde se guardan los usuarios conectados*************/
        /***********Evento cuando se conecta el usuario***************/
        public ChatHub(HandlerMessagesUseCase handlerMessagesUseCase)
        {
            _handlerMessagesUseCase = handlerMessagesUseCase;
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        /**************Evento de usuario conectado, para mandarlo a todos********************/
        public async Task UserOnline(Guid UserId)
        {
            await _handlerMessagesUseCase.onLoginUser(UserId, Context.ConnectionId);
            await Clients.All.SendAsync("ListUpdate", await _handlerMessagesUseCase.UpdatedListOfUsers());
        }
        /****************Desconexion de todos los usuarios********************/
        public async Task UserOffline(Guid UserId)
        {
            await _handlerMessagesUseCase.onLogOutUser(UserId);
            await Clients.All.SendAsync("ListUpdate", await _handlerMessagesUseCase.UpdatedListOfUsers());
        }
        /**************Se borra al usuario de la lista para mantener actualizado***************/
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await _handlerMessagesUseCase.onLogOutUser(Context.ConnectionId);
            await Clients.All.SendAsync("ListUpdate", await _handlerMessagesUseCase.UpdatedListOfUsers());
            await base.OnDisconnectedAsync(exception);
        }
        /***************Evento de Usuario envio mensaje****************/
        public async Task SendMessage(SendMessageDto messageDto)
        {
            if (messageDto.IdReciber is not null)
            {
                (string IdMessageFrom, MessageDto message) = await _handlerMessagesUseCase.SendMessage(messageDto);
                if (!string.IsNullOrEmpty(IdMessageFrom))
                    await Clients.Client(IdMessageFrom).SendAsync("MessageRecibed", message);
            }
            else
            {
                (string NameGroup, MessageDto message) = await _handlerMessagesUseCase.SendMessage(messageDto);
                await Clients.Group(NameGroup).SendAsync("MessageRecibed", message);
            }
        }

    }
}
