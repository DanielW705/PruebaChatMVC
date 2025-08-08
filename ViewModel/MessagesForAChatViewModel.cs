using PruebaChatMVC.Dto;
using System;
using System.Collections.Generic;

namespace PruebaChatMVC.ViewModel
{
    public class MessagesForAChatViewModel
    {
        public Guid ActualUser {  get; set; }
        public Guid ActualChat { get; set; }
        public Guid? UsuarioChat { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
