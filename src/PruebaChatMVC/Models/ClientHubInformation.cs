using LibreriaChatMVC.Entities;

namespace PruebaChatMVC.Models
{
    public class ClientHubInformation
    {
        private readonly UserDto _userInformation;
        public string UserId => _userInformation.Id;
        public string? ContextId { get; set; }
        public bool isLogin { get; set; }
        public bool isConnected { get; set; }
        public ClientHubInformation(UserDto userInformation)
        {
            _userInformation = userInformation;
            isLogin = true;
            isConnected = false;
        }

    }
}
