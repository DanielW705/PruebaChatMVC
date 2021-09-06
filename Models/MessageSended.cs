using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaChatMVC.Models
{
    public class MessageSended
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

    }
}
