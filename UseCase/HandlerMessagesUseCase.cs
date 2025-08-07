using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PruebaChatMVC.Data;
using PruebaChatMVC.Dto;
using ROP;
using System.Collections.Generic;

namespace PruebaChatMVC.UseCase
{
    public class HandlerMessagesUseCase : HandlerCookieInformationUseCase
    {
        public static List<UserDto> UsersConnected = new List<UserDto>();
        private readonly ILogger<HandlerMessagesUseCase> _logger;
        private readonly ChatPruebaDbContext _chatPruebaDbContext;
        public HandlerMessagesUseCase(ILogger<HandlerMessagesUseCase> logger, ChatPruebaDbContext chatPruebaDbContext, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _logger = logger;
            _chatPruebaDbContext = chatPruebaDbContext;
        }


        
        private void SaveUserLogging(UserDto user) => UsersConnected.Add(user);

        public void LoginUser(UserDto user) => SaveUserLogging(user);

    }
}
