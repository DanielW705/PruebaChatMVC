using System;

namespace PruebaChatMVC.Models
{
    public class Participants : SoftDelete
    {
        public int IdParticipants { get; set; }

        public Guid IdChat { get; set; }

        public Guid IdUser { get; set; }

        public Users UserInTheChat { get; set; }

        public Chats Chat { get; set; }

    }
}
