using System;

namespace PruebaChatMVC.Models
{
    public class UsersConnected : SoftDelete
    {
        public Guid IdUser { get; set; }

        public string? Idconetxt { get; set; }

        public bool IsConnected { get; set; }

        public DateTime LastConnected { get; set; }

        public Users UserConnected { get; set; }
    }
}
