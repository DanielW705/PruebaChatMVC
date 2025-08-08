using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private readonly ILogger<HandlerMessagesUseCase> _logger;
        private readonly ChatPruebaDbContext _chatPruebaDbContext;
        public HandlerMessagesUseCase(ILogger<HandlerMessagesUseCase> logger, ChatPruebaDbContext chatPruebaDbContext, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _logger = logger;
            _chatPruebaDbContext = chatPruebaDbContext;
        }
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
        private async Task<Result<string>> GetSpecificUserIdContext(Guid Iduser) => (await _chatPruebaDbContext.UsersConnected
                                                                                     .FirstOrDefaultAsync(uc => uc.IdUser.Equals(Iduser)))
                                                                                     .Idconetxt;

        private async Task<Result<UsersConnected>> GetSpecificUserConnected(UserDto user) => await _chatPruebaDbContext.UsersConnected
                                                                                                   .FirstOrDefaultAsync(uc => uc.IdUser.Equals(user.IdUsuario));

        private async Task<string> GetGroupName(Guid idGroup) => (await _chatPruebaDbContext.Chats
                                                                        .SingleAsync(c => c.IdChat.Equals(idGroup)))
                                                                        .ChatName;

        private async Task<Result<List<UserDto>>> GetAllUsers() => await _chatPruebaDbContext.Usuarios
                                                                         .Include(u => u.UserIsConnected)
                                                                         .Where(u => u.UserIsConnected.IsConnected)
                                                                         .Select(u => new UserDto(u.IdUser, u.UserName))
                                                                         .ToListAsync();

        private async Task<(string, Result<Messages>)> ConstructMessage(SendMessageDto newMessage, Messages message)
        {
            (string, Result<Messages>) output;
            if (newMessage.IdReciber != null)
            {
                string IdContext = await GetSpecificUserIdContext(newMessage.IdReciber ?? Guid.Empty).ThrowAsync();
                output = (IdContext, message);
            }
            else
            {
                string chatName = await GetGroupName(newMessage.IdChatSended);
                output = (chatName, message);
            }
            return output;
        }
        private async Task<Result<Unit>> SaveUserOnList(UserDto user, string context)
        {
            Result<Unit> output = Result.Unit;

            UsersConnected userConnected = await GetSpecificUserConnected(user).ThrowAsync();

            if (userConnected is not null)
            {
                try
                {
                    userConnected.Idconetxt = context;
                    userConnected.IsConnected = true;
                    await _chatPruebaDbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    string exceptionJson = JsonConvert.SerializeObject(ex);
                    output = Result.Failure<Unit>(exceptionJson);
                }
            }
            else
            {
                try
                {
                    userConnected = new UsersConnected() { IdUser = user.IdUsuario, Idconetxt = context };
                }
                catch (Exception ex)
                {
                    string exceptionJson = JsonConvert.SerializeObject(ex);
                    output = Result.Failure<Unit>(exceptionJson);
                }

            }
            return output;
        }
        private async Task<Result<Unit>> DisconectUserConnected(UserDto user)
        {
            Result<Unit> output = Result.Unit;
            UsersConnected userConnected = await GetSpecificUserConnected(user).ThrowAsync();
            try
            {
                userConnected.Idconetxt = null;
                userConnected.IsConnected = false;
                await _chatPruebaDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string exceptionJson = JsonConvert.SerializeObject(ex);
                output = Result.Failure<Unit>(exceptionJson);
            }
            return output;
        }
        private async Task<Result<Unit>> EliminateUserWithContextList(string context)
        {
            Result<Unit> output = Result.Unit;
            UsersConnected userConnected = await _chatPruebaDbContext.UsersConnected
                                                 .FirstAsync(uc => uc.Idconetxt.Equals(context));
            try
            {
                userConnected.Idconetxt = null;
                userConnected.IsConnected = false;
                await _chatPruebaDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string exceptionJson = JsonConvert.SerializeObject(ex);
                output = Result.Failure<Unit>(exceptionJson);
            }
            return output;
        }
        private void LogLastError(string exceptionJson)
        {
            Exception exception = JsonConvert.DeserializeObject<Exception>(exceptionJson);

            _logger.LogError(exception, "Error");
        }

        public async Task onLogOutUser(UserDto user)
        {
            var response = await DisconectUserConnected(user);
            if (!response.Success)
            {
                string Json = response.Errors.FirstOrDefault().Message;
                LogLastError(Json);
            }
        }
        public async Task onLogOutUser(string context)
        {
            var response = await EliminateUserWithContextList(context);
            if (!response.Success)
            {
                string Json = response.Errors.FirstOrDefault().Message;
                LogLastError(Json);
            }
        }
        public async Task onLoginUser(UserDto user, string context)
        {
            var response = await SaveUserOnList(user, context);
            if (!response.Success)
            {
                string Json = response.Errors.FirstOrDefault().Message;
                LogLastError(Json);
            }
        }
        public async Task<(string, Result<Messages>)> SendMessage(SendMessageDto message) => await SaveAMessage(message).Map(m => ConstructMessage(message, m)).ThrowAsync();

        public async Task<List<UserDto>> UpdatedListOfUsers() => await GetAllUsers().ThrowAsync();

    }
}
