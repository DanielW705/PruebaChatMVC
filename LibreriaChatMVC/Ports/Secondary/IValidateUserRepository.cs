using LibreriaChatMVC.Entities;

namespace LibreriaChatMVC.Ports.Secondary
{
    public interface IValidateUserRepository
    {
        Task<UserDto?> ValidateUserExist(string username, string password);
    }
}
