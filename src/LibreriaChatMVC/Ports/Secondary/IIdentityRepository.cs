using LibreriaChatMVC.Entities;

namespace LibreriaChatMVC.Ports.Secondary
{
    public interface IIdentityRepository
    {
        UserDto? GetUserInformation();
        Task SignInUserAsync(string userId, string userName, string role);
        Task SingOutAsync();
    }
}
