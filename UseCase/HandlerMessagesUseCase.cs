using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaChatMVC.Data;
using PruebaChatMVC.Dto;
using PruebaChatMVC.Models;
using ROP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaChatMVC.UseCase
{
    public class HandlerMessagesUseCase : HandlerCookieInformationUseCase
    {
        private static Dictionary<Guid, string> _UsersConnected = new Dictionary<Guid, string>();

        public Dictionary<Guid, UserDto> UsersConnected = new Dictionary<Guid, UserDto>();

        private readonly ILogger<HandlerMessagesUseCase> _logger;
        private readonly ChatPruebaDbContext _chatPruebaDbContext;
        public HandlerMessagesUseCase(ILogger<HandlerMessagesUseCase> logger, ChatPruebaDbContext chatPruebaDbContext, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _logger = logger;
            _chatPruebaDbContext = chatPruebaDbContext;
        }
        public string GetSpecificContext(Guid key) => _UsersConnected[key];
        private async Task<Result<Messages>> SaveAMessage(SendMessageDto newMessage)
        {
            Messages output = new Messages()
            {
                Message = newMessage.Message,
                IdUserSender = newMessage.IdUserSender,
                IdChatSended = newMessage.IdChatSended,
                SendTime = DateTime.Now,
                visto = false
            };
            try
            {
                _chatPruebaDbContext.Mensajes.Add(output);
                await _chatPruebaDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar mensaje");
            }
            return output;
        }
        private async Task<string> GetGroupName(Guid idGroup) => (await _chatPruebaDbContext.Chats.SingleAsync(c => c.IdChat.Equals(idGroup))).ChatName;

        private async Task<(string, Result<Messages>)> ConstructMessage(SendMessageDto newMessage, Messages message)
        {
            (string, Result<Messages>) output;
            if (newMessage.IdReciber is not null)
            {
                string idContext = GetSpecificContext(newMessage.IdReciber ?? Guid.Empty);
                output = (idContext, message);
            }
            else
            {
                string chatName = await GetGroupName(newMessage.IdChatSended);
                output = (chatName, message);
            }
            return output;
        }
        private void SaveUserOnList(UserDto user, string context)
        {
            _UsersConnected.Add(user.IdUsuario, context);
            UsersConnected.Add(user.IdUsuario, user);
        }
        private void EliminateUserOnList(UserDto user)
        {
            _UsersConnected.Remove(user.IdUsuario);
            UsersConnected.Remove(user.IdUsuario);
        }
        private void EliminateUserWithContextList(string context)
        {
            Guid idUser = _UsersConnected.FirstOrDefault(x => x.Value.Equals(context)).Key;
            _UsersConnected.Remove(idUser);
            UsersConnected.Remove(idUser);
        }

        public void onLogOutUser(UserDto user) => EliminateUserOnList(user);
        public void onLogOutUser(string context) => EliminateUserWithContextList(context);
        public void onLoginUser(UserDto user, string context) => SaveUserOnList(user, context);

        public async Task<(string, Result<Messages>)> SendMessage(SendMessageDto message) => await SaveAMessage(message).Map(m => ConstructMessage(message, m)).ThrowAsync();

    }
}
