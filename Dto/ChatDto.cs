using PruebaChatMVC.Enums;
using System;

namespace PruebaChatMVC.Dto
{
    public record class ChatDto(Guid IdChat, string ChatName, TypeOfChat TypeOfChat);
}
