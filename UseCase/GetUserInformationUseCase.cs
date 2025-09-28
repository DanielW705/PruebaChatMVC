using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PruebaChatMVC.Data;
using PruebaChatMVC.Dto;
using PruebaChatMVC.Extensions;
using PruebaChatMVC.ViewModel;
using ROP;
using System;
using System.Threading.Tasks;

namespace PruebaChatMVC.UseCase
{
    public class GetUserInformationUseCase : HandlerCookieInformationUseCase
    {
        private readonly ChatPruebaDbContext _chatPruebaDbContext;
        public GetUserInformationUseCase(ChatPruebaDbContext chatPruebaDbContext, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _chatPruebaDbContext = chatPruebaDbContext;
        }

        private async Task<UserDto> GetUser(Guid idUser) => (await _chatPruebaDbContext.Usuarios.FirstOrDefaultAsync(u => u.IdUser.Equals(idUser))).ToDto();

        public async Task<UserDto> Execute()
        {
            UserDto output = null;

            Guid? idUser = GetUserInformationSession();
            
            if(idUser is not null)
            {
                output = await GetUser(idUser.Value);
            }

            return output;
        }
    }
}
