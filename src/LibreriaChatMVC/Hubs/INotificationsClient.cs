using LibreriaChatMVC.Entities;

namespace LibreriaChatMVC.Hubs
{
    public interface INotificationsClient
    {
        Task OnNewUserLogIn(UserDto userDto);
        Task OnNewUserConnect(UserDto userDto);
    }
}
