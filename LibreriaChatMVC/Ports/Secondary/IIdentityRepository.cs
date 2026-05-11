using LibreriaChatMVC.Entities;

namespace LibreriaChatMVC.Ports.Secondary
{
    public interface IIdentityRepository
    {
        UserDto? GetUserInformation();
        Task SignInUser(string userId, string userName, string role);
        Task SingOut();
    }
}
