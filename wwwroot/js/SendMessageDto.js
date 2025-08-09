export class SendMessageDto {
    constructor(Message, IdUserSender, IdChatSended, IdReciber) {
        this.Message = Message;
        this.IdUserSender = IdUserSender;
        this.IdChatSended = IdChatSended;
        this.IdReciber = IdReciber;
    }
}