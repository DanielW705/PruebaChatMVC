"use strict"
class ChatHub {
    #SignalrHub;
    constructor() {
        this.#SignalrHub = new signalR
            .HubConnectionBuilder()
            .configureLogging(signalR.LogLevel.Error)
            .withUrl("/chatHub")
            .build();
    }
    #SendUserLogin(userId) {
        this.#SignalrHub.invoke("UserOnline", userId);
    }
    StartConnection(userId) {
        this.#SignalrHub.start().then(() => this.#SendUserLogin(userId))
    }
    SendAMessage(SendMessageDto) {
        this.#SignalrHub.invoke("SendMessage", SendMessageDto);
    }
    RecibedMessage(callback) {
        this.#SignalrHub.on("MessageRecibed", callback);
    }
}

export { ChatHub }