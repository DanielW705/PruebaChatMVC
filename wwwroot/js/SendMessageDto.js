export class SendMessageDto {
    constructor(Message, IdUserSender, IdChatSended, IdReciber, Sended) {
        this.Message = Message;
        this.IdUserSender = IdUserSender;
        this.IdChatSended = IdChatSended;
        this.IdReciber = IdReciber;
        this.Sended = Date.now();
    }
}