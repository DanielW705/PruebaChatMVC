using LibreriaChatMVC.Models;

namespace LibreriaChatMVC.Seeders
{
    public class ChatIndividualSeeder : ISeeder<ChatIndividual, Usuario[]>
    {
        public ChatIndividual[] ApplySeed(Usuario[] seeds)
        {
            ChatIndividual[] output =
             [
                new ChatIndividual
                {
                    IdChat = Guid.NewGuid(),
                    Emisor = seeds.First().IdUsuario,
                    Receptor = seeds.Skip(1).First().IdUsuario
                },
                new ChatIndividual
                {
                    IdChat = Guid.NewGuid(),
                    Emisor = seeds.Skip(1).First().IdUsuario,
                    Receptor = seeds.Skip(2).First().IdUsuario,
                },
            ];
            return output;
        }
    }
}
