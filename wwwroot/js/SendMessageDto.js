export class SendMessageDto {
    Message;
    IdUserSender;
    IdChatSended;
    IdReciber;
    Sended;

    static ToDto(data) {
        var sendMessageDto = Object.assign(this, data)
        return sendMessageDto;
    }
    constructor(message, idUserSender, idChatSended, idReciber, sended) {
        this.Message = message;
        this.IdUserSender = idUserSender;
        this.IdChatSended = idChatSended;
        this.IdReciber = idReciber;
        this.Sended = sended;
    }
}