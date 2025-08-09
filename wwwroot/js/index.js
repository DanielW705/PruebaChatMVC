'use strict';
import { ChatHub } from "./Chat.js";
import { SendMessageDto } from "./SendMessageDto.js";
import { TypeOfMessage } from "./TypeOfMessage.js";
const chat = new ChatHub();

const ChatsIcon = document.querySelectorAll(".ChatBody .ChatsAvailableAside .ListOfChats .Chat form");
ChatsIcon.forEach(icon => icon.addEventListener('submit', async (e) => await MakeFetch(e)));

let ul = null;

const ConstructMessage = (message, type) => {
    const li = document.createElement("li");

    li.textContent = message;

    switch (type) {
        case TypeOfMessage.Sender:
            li.classList.add("MessageSent");
            break;
        case TypeOfMessage.Reciver:
            li.classList.add("MessageRecived");
            break;
    }

    return li;
}

const HandlerRecibeMessages = (data) => {
    ul.appendChild(ConstructMessage(data.message, TypeOfMessage.Reciver))
}


chat.RecibedMessage(HandlerRecibeMessages);

const ParseTxt = (text) => {
    const parser = new DOMParser();

    let doc = parser.parseFromString(text, 'text/html');

    return doc.body;
}
const CreateBody = (data) => {
    const bodyCreated = {
        IdChat: data.dataset.idchat,
        IdTypeOfChat: data.dataset.idtypeofchat,
        ChatName: data.dataset.chatname
    }

    return JSON.stringify(bodyCreated);
}

const ConfigureOptions = (data) => ({
    method: 'POST',
    body: CreateBody(data),
    headers: {
        'Content-Type': 'application/json'
    },
    redirect: "follow"
});

const onSendMessage = (e) => {
    e.preventDefault();
    const actualUser = e.target.dataset.actualuser;
    const actualChat = e.target.dataset.actualchat;
    const usuarioChat = e.target.dataset.usuariochat;
    const input = e.target.elements.Message;
    const message = input.value;

    if (message !== '') {
        const sendMessageDto = new SendMessageDto(message, actualUser, actualChat, usuarioChat);
        input.value = '';
        chat.SendAMessage(sendMessageDto);
        ul.appendChild(ConstructMessage(message, TypeOfMessage.Sender));

    }

}

const MakeFetch = async (e) => {
    e.preventDefault();
    const MessageBody = document.querySelector(".MessagesBody");
    const response = await fetch(e.target.action, ConfigureOptions(e.target));
    const textResponse = await response.text();
    const result = ParseTxt(textResponse);
    MessageBody.appendChild(result);

    const form = result.querySelector("form");

    const id = form.dataset.actualuser;

    chat.StartConnection(id);

    ul = form.querySelector("ul");

    result.addEventListener('submit', onSendMessage)
};
