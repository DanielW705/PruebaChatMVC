using PruebaChatMVC.Models;
using System;
using PruebaChatMVC.Enums;
namespace PruebaChatMVC.Seeders
{
    public class ChatSeeder : ISeeder<Chats>
    {
        public Chats ApplySeed()
        {
            return new Chats()
            {
                IdChat = Guid.Parse("d7917cfe-0e42-4f57-b237-8a44ae5d3d20"),
                ChatName = "Chat de Daniel y Julio",
                ChatDescription = null,
                TypeOfChat = TypeOfChat.Individual
            };
        }
    }
}
