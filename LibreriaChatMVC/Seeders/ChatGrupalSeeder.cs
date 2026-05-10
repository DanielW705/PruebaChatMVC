using LibreriaChatMVC.Models;

namespace LibreriaChatMVC.Seeders
{
    internal class ChatGrupalSeeder : ISeeder<ChatGrupal, Usuario[]>
    {
        public ChatGrupal[] ApplySeed(Usuario[] seed)
        {
            Guid Id = Guid.NewGuid();
            return [
                new ChatGrupal
                {
                    IdChat = Id,
                }
            ];
        }
    }
}
