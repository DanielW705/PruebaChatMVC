using LibreriaChatMVC.Entities;
using LibreriaChatMVC.ViewModels;
using ROP;
namespace LibreriaChatMVC.Ports.Primary
{
    public interface ILoginServices
    {
        Task<Result<UserDto>> TryToLoginAsync(UsuarioViewModel user, CancellationToken ctoken);
        Task LogOutAsync(string id, CancellationToken ctoken);
        Task<Result<UserDto>> RegisterAndLogInAsync(UsuarioViewModel user, CancellationToken ctoken);
    }
}
