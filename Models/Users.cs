using System;
using System.Collections.Generic;

namespace PruebaChatMVC.Models
{
    public class Users : SoftDelete
    {
        public Guid IdUser { get; set; }
        public string pasword;
        public string UserName { get; set; }
        public ICollection<Participants> ParticipantInChat { get; set; }

        public ICollection<Messages> UserSendMessage { get; set; }

        public UsersConnected UserIsConnected { get; set; }
    }
}
