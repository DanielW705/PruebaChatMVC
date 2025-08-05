using System;
using System.Collections.Generic;

namespace PruebaChatMVC.Models
{
    public class UserChat : SoftDelete
    {
        public int IdChat { get; set; }
        public Guid idUser1 { get; set; }

        public Guid idUser2 { get; set; }

        public User rel_User1_User2 { get; set; }

        public User rel_User2_User1 { get; set; }

        public ICollection<Messages> Messages { get; set; }
    }
}
