using PruebaChatMVC.Enums;
using System;
using System.Collections.Generic;

namespace PruebaChatMVC.Models
{
    public class Chats : SoftDelete
    {
        public Guid IdChat { get; set; }
        public string ChatName { get; set; }
        public string ChatDescription { get; set; }
        public TypeOfChat TypeOfChat { get; set; }
        public ICollection<Participants> ChatParticipants { get; set; }
        public ICollection<Messages> MessagesSendedToThisChat { get; set; }
    }
}
