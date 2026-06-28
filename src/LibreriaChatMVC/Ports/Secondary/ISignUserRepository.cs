using LibreriaChatMVC.Entities;

namespace LibreriaChatMVC.Ports.Secondary
{
    public interface ISignUserRepository
    {
        Task<UserDto?> SignInUserAsync(string username, string password, CancellationToken ctoken);
        Task<bool> SignOutUserAsync(string id, CancellationToken ctoken);
        Task<UserDto> CreateNewUserAsync(string username, string password, CancellationToken ctoken);
    }
}
