using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaChatMVC.Models
{
    public class UserChat
    {
        public Guid idUser { get; set; }
        public string UserName { get; set; }
        public string idChat { get; set; }
        public User relChat_User { get; set; }
        public UserChat()
        {

        }
        public UserChat(Guid _idUser, string _Username, string _idChat)
        {
            this.idUser = _idUser;
            this.UserName = _Username;
            this.idChat = _idChat;
        }
    }
}
