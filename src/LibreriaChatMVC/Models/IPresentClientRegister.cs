using LibreriaChatMVC.Entities;
namespace LibreriaChatMVC.Models
{
    public interface IPresentClientRegister
    {
        void Add(UserDto newUser);
        bool TryGetUserId(string contextId, out string userId);
        bool TryGetConnectionId(string userId, out string contextId);
        void UpdateConnectionId(string userId, string contextId);
        bool RemoveUser(string userId);
        void DisconectUser(string contextId);
    }
}
