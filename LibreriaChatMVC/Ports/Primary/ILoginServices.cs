using LibreriaChatMVC.Entities;
using LibreriaChatMVC.ViewModels;
using ROP;
namespace LibreriaChatMVC.Ports.Primary
{
    public interface ILoginServices
    {
        Task<Result<UserDto>> TryToLogin(UsuarioViewModel user);
    }
}
