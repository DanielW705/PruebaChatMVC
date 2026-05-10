using LibreriaChatMVC.Models;

namespace LibreriaChatMVC.Seeders
{
    public class MensajesSeeder : ISeeder<Mensajes, ChatBase[], Usuario[]>
    {
        public Mensajes[] ApplySeed(ChatBase[] seed1, Usuario[] seed2)
        {
            return seed1.SelectMany((chat, index) => chat switch
            {
                ChatIndividual => [
                    new Mensajes
                {
                    IdMensaje = Guid.NewGuid(),
                    Mensaje = index > 0? "Hola mundo": "Respuesta del mundo :)" ,
                    IdChat = chat.IdChat,
                    IdEmisor = seed2.Skip(index).First().IdUsuario
                }],
                ChatGrupal => [new Mensajes {
                             IdMensaje = Guid.NewGuid(),
                           IdChat = chat.IdChat,
                           IdEmisor = seed2.First().IdUsuario,
                           Mensaje = "Hola a todos"
                }],
                _ => Array.Empty<Mensajes>()
            }).ToArray();
        }
    }
}
