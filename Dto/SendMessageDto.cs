using System;

namespace PruebaChatMVC.Dto
{
    public record class SendMessageDto(string Message, Guid IdUserSender, Guid IdChatSended, Guid? IdReciber);
}
