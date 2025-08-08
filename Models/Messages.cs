using System;

namespace PruebaChatMVC.Models
{
    public class Messages : SoftDelete
    {
        public Guid idMessage { get; set; }
        public string Message { get; set; }
        public Guid IdUserSender { get; set; }
        public Guid IdChatSended { get; set; }
        public DateTime SendTime { get; set; }
        public bool visto { get; set; }
        public Users UserSended { get; set; }
        public Chats ChatSended { get; set; }

    }
}
