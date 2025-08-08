using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Text.Json;
using PruebaChatMVC.Models;
using PruebaChatMVC.Data;
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
        public async Task UserOnline(UserDto userDto)
        {
            await _handlerMessagesUseCase.onLoginUser(userDto, Context.ConnectionId);
            await Clients.All.SendAsync("ListUpdate", await _handlerMessagesUseCase.UpdatedListOfUsers());
        }
        /****************Desconexion de todos los usuarios********************/
        public async Task UserOffline(UserDto userDto)
        {
            await _handlerMessagesUseCase.onLogOutUser(userDto);
            await Clients.All.SendAsync("ListUpdate", await _handlerMessagesUseCase.UpdatedListOfUsers());
        }
        /**************Se borra al usuario de la lista para mantener actualizado***************/
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await _handlerMessagesUseCase.onLogOutUser(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
        /***************Evento de Usuario envio mensaje****************/
        public async Task SendMessage(SendMessageDto messageDto)
        {
            if (messageDto.IdReciber is not null)
            {
                (string IdMessageFrom, Result<Messages> message) = await _handlerMessagesUseCase.SendMessage(messageDto);
                await Clients.Client(IdMessageFrom).SendAsync("MessageRecibed", message);
            }
            else
            {
                (string NameGroup, Result<Messages> message) = await _handlerMessagesUseCase.SendMessage(messageDto);
                await Clients.Group(NameGroup).SendAsync("MessageRecibed", message);
            }
        }

    }
}
