using System;
using System.Collections.Generic;

namespace PruebaChatMVC.Models
{
    public class Messages : SoftDelete
    {
        public Guid idMensaje { get; set; }
        public string nameSender { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaDeEnvio { get; set; }
        public bool visto { get; set; }
        public Guid Sender { get; set; }
        public Guid Reciber { get; set; }
        public User relSender_User { get; set; }
        public User relReciver_User { get; set; }

        public int IdChatSended { get; set; }

        public UserChat relMensaje_Chat { get; set; }

    }
}
