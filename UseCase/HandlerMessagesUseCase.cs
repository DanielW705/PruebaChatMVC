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
        private async Task<Result<UsersConnected>> GetSpecificUserIdContext(Guid userId) => await _chatPruebaDbContext.UsersConnected
                                                                                     .FirstOrDefaultAsync(uc => uc.IdUser.Equals(userId));

        private async Task<Result<UsersConnected>> GetSpecificUserConnected(Guid userId) => await _chatPruebaDbContext.UsersConnected
                                                                                                   .FirstOrDefaultAsync(uc => uc.IdUser.Equals(userId));

        private async Task<string> GetGroupName(Guid idGroup) => (await _chatPruebaDbContext.Chats
                                                                        .SingleAsync(c => c.IdChat.Equals(idGroup)))
                                                                        .ChatName;

        private async Task<Result<List<UserDto>>> GetAllUsers() => await _chatPruebaDbContext.Usuarios
                                                                         .Include(u => u.UserIsConnected)
                                                                         .Where(u => u.UserIsConnected.IsConnected)
                                                                         .Select(u => new UserDto(u.IdUser, u.UserName))
                                                                         .ToListAsync();

        private async Task<(string, MessageDto)> ConstructMessage(SendMessageDto newMessage)
        {
            (string, MessageDto) output;
            if (newMessage.IdReciber != null)
            {
                UsersConnected usersConnected = await GetSpecificUserIdContext(newMessage.IdReciber ?? Guid.Empty).ThrowAsync();

                output = (usersConnected?.Idconetxt ?? string.Empty, new MessageDto(newMessage.Message, newMessage.IdUserSender, newMessage.IdChatSended, DateTime.Now));
            }
            else
            {
                string chatName = await GetGroupName(newMessage.IdChatSended);
                output = (chatName, new MessageDto(newMessage.Message, newMessage.IdUserSender, newMessage.IdChatSended, DateTime.Now));
            }
            return output;
        }
        private async Task<Result<Unit>> SaveUserOnList(Guid userId, string context)
        {
            Result<Unit> output = Result.Unit;

            UsersConnected userConnected = await GetSpecificUserConnected(userId).ThrowAsync();

            if (userConnected is not null)
            {
                try
                {
                    userConnected.Idconetxt = context;
                    userConnected.IsConnected = true;
                    userConnected.LastConnected = DateTime.Now;
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
                    userConnected = new UsersConnected() { IdUser = userId, Idconetxt = context };
                    _chatPruebaDbContext.UsersConnected.Add(userConnected);
                    await _chatPruebaDbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    string exceptionJson = JsonConvert.SerializeObject(ex);
                    output = Result.Failure<Unit>(exceptionJson);
                }

            }
            return output;
        }
        private async Task<Result<Unit>> DisconectUserConnected(Guid userId)
        {
            Result<Unit> output = Result.Unit;
            UsersConnected userConnected = await GetSpecificUserConnected(userId).ThrowAsync();
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

        public async Task onLogOutUser(Guid userId)
        {
            var response = await DisconectUserConnected(userId);
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
        public async Task onLoginUser(Guid userId, string context)
        {
            var response = await SaveUserOnList(userId, context);
            if (!response.Success)
            {
                string Json = response.Errors.FirstOrDefault().Message;
                LogLastError(Json);
            }
        }
        public async Task<(string, MessageDto)> SendMessage(SendMessageDto message) => await SaveAMessage(message).Map(m => ConstructMessage(message)).ThrowAsync();

        public async Task<List<UserDto>> UpdatedListOfUsers() => await GetAllUsers().ThrowAsync();

    }
}
