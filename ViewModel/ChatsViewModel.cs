using PruebaChatMVC.Dto;
using System;
using System.Collections.Generic;

namespace PruebaChatMVC.ViewModel
{
    public class ChatsViewModel
    {
        public Guid userId { get; set; }
        public List<ChatDto> Chats { get; set; }
    }
}
