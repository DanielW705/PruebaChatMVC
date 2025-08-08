using System;

namespace PruebaChatMVC.Dto
{
    public record class MessageDto(string Message, Guid IdUserSender, Guid IdChatSended);
}
