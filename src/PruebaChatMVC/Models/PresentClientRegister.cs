using LibreriaChatMVC.Entities;
using LibreriaChatMVC.Models;

namespace PruebaChatMVC.Models
{
    public class PresentClientRegister : IPresentClientRegister
    {
        private readonly Dictionary<string, ClientHubInformation> _clientsInformation;
        private readonly ILogger _logger;
        public PresentClientRegister(ILogger<PresentClientRegister> logger)
        {
            _clientsInformation = new Dictionary<string, ClientHubInformation>();
            _logger = logger;
        }
        public void Add(UserDto newUser) => _clientsInformation.Add(newUser.Id, new ClientHubInformation(newUser));
        public bool RemoveUser(string userId)
        {
            bool output = false;
            if (_clientsInformation.ContainsKey(userId))
            {
                _clientsInformation.Remove(userId);
                output = true;
            }
            return output;
        }
        public void DisconectUser(string contextId)
        {
            if (_clientsInformation.TryGetValue(contextId, out var clientInformation))
                clientInformation.isConnected = false;
            else
                _logger.LogWarning("El usuario que buscas no se a registrado");
        }
        public bool TryGetConnectionId(string userId, out string contextId)
        {
            bool output = false;
            contextId = string.Empty;
            if (_clientsInformation.TryGetValue(userId, out var clientInformation) && clientInformation.ContextId is not null)
            {
                contextId = clientInformation.ContextId;
                output = true;
            }
            return output;
        }
        public bool TryGetUserId(string contextId, out string userId)
        {
            bool output = false;
            userId = string.Empty;
            if (_clientsInformation.TryGetValue(contextId, out var clientInformation))
            {
                userId = clientInformation.UserId;
                output = true;
            }
            return output;
        }
        public void UpdateConnectionId(string userId, string contextId)
        {
            if (_clientsInformation.TryGetValue(userId, out var clientInformation))
                clientInformation.ContextId = contextId;
            else
                _logger.LogWarning("El usuario que buscas no se a registrado");
        }
    }
}
