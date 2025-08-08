using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaChatMVC.Data;
using PruebaChatMVC.Models;
using PruebaChatMVC.ViewModel;
using ROP;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using PruebaChatMVC.Dto;
using System.Linq;
using System.Collections.Generic;

namespace PruebaChatMVC.UseCase
{
    public class HanderUserUseCase : HandlerCookieInformationUseCase
    {
        private readonly ChatPruebaDbContext _chatPruebaDbContext;
        private readonly ILogger<HanderUserUseCase> _logger;
        public HanderUserUseCase(ChatPruebaDbContext chatPruebaDbContext, ILogger<HanderUserUseCase> logger, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _chatPruebaDbContext = chatPruebaDbContext;
            _logger = logger;
        }

        private async Task<Result<Unit>> FindUserCase(LoginRegisterUserViewModel user)
        {
            Result<Unit> output;
            Users usuario = await _chatPruebaDbContext.Usuarios.FirstOrDefaultAsync(
                u => u.UserName.Equals(user.UserName)
                && u.pasword.Equals(user.Pasword));
            if (usuario is not null)
            {
                SignUserId(usuario.IdUser);
                output = Result.Unit;
            }
            else
                output = Result.Failure<Unit>("El usuario no existe");

            return output;
        }

        private async Task<Result<Unit>> SaveNewUser(LoginRegisterUserViewModel newUser)
        {
            Result<Unit> output;
            try
            {
                Users newUsers = new Users() { UserName = newUser.UserName, pasword = newUser.Pasword };
                _chatPruebaDbContext.Usuarios.Add(newUsers);
                await _chatPruebaDbContext.SaveChangesAsync();
                output = Result.Unit;
                SignUserId(newUsers.IdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");

                output = Result.Failure<Unit>("Error en el servidor");
            }
            return output;
        }

        private async Task<Result<List<ChatDto>>> GetAllChatByUser(Guid userId) => await _chatPruebaDbContext.Chats
                                                                                   .Include(c => c.ChatParticipants
                                                                                   .Where(p => p.IdUser.Equals(userId)))
                                                                                    .Select(c => new ChatDto(c.IdChat, c.ChatName, c.TypeOfChat))
                                                                                    .ToListAsync();

        private async Task<Result<List<MessageDto>>> GetFirstNMessagesForAChat(ChatDto chat, int n) => await _chatPruebaDbContext.Mensajes
                                                                                       .Where(m => m.IdChatSended.Equals(chat.IdChat))
                                                                                       .Take(n)
                                                                                       .Select(m => new MessageDto(m.Message, m.IdUserSender, m.IdChatSended))
                                                                                       .ToListAsync();

        private async Task<Result<Guid?>> GetReciber(ChatDto chat, Guid actualUser)
        {
            Guid? output = null;

            if (chat.TypeOfChat is Enums.TypeOfChat.Individual)
                output = await _chatPruebaDbContext.Participantes
                         .Where(p => p.IdChat.Equals(chat.IdChat) && !p.IdUser.Equals(actualUser))
                         .Select(p => p.IdChat)
                         .SingleAsync();

            return output;
        }
        public async Task<Result<Unit>> Login(LoginRegisterUserViewModel user) => await FindUserCase(user);

        public async Task<Result<Unit>> RegisterNewuser(LoginRegisterUserViewModel user) => await SaveNewUser(user);

        public async Task<Result<ChatsViewModel>> Execute() => await GetUserInformation()
                                                                        .Bind(x => GetAllChatByUser(x)
                                                                        .Map(l => new ChatsViewModel { Chats = l }));

        public async Task<Result<MessagesForAChatViewModel>> GetMessages(ChatDto chat) => await GetFirstNMessagesForAChat(chat, 10)
                                                                                                   .Bind(ms => GetUserInformation()
                                                                                                   .Bind(i => GetReciber(chat, i)
                                                                                                   .Map(r => new MessagesForAChatViewModel { Messages = ms, ActualUser = i, UsuarioChat = r })));
    }
}
