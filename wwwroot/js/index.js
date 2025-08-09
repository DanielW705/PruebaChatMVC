'use strict';
import { ChatHub } from "./Chat.js";
import { SendMessageDto } from "./SendMessageDto.js";
import { TypeOfMessage } from "./TypeOfMessage.js";

const chat = new ChatHub();

const chatBody = document.querySelector('.chatBody')

const ChatsIcon = chatBody.querySelectorAll(".chatsAvailableAside .listOfChats .chat .chatButton");

const OnLoadWindow = (chatBody) => {
    const { userid } = chatBody.dataset;

    chat.StartConnection(userid);
}



OnLoadWindow(chatBody);


const ConstructMessage = (message, type) => {
    const li = document.createElement("li");

    li.textContent = message;

    switch (type) {
        case TypeOfMessage.Sender:
            li.classList.add("messageSent");
            break;
        case TypeOfMessage.Reciver:
            li.classList.add("messageRecived");
            break;
    }

    return li;
}

const HandlerRecibeMessages = (data) => {
    ul.appendChild(ConstructMessage(data.message, TypeOfMessage.Reciver))
}

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

const onSendMessage = (e, chat, ul) => {
    e.preventDefault();
    const { actualuser, actualchat, usuariochat } = e.target.dataset;
    const input = e.target.elements.Message;
    const message = input.value;

    if (message !== '') {
        const sendMessageDto = new SendMessageDto(message, actualuser, actualchat, usuariochat);
        input.value = '';
        chat.SendAMessage(sendMessageDto);
        ul.appendChild(ConstructMessage(message, TypeOfMessage.Sender));
    }

}



const MakeFetch = async (e) => {
    e.preventDefault();
    const MessageBody = document.querySelector(".messagesBody");
    const response = await fetch(e.target.action, ConfigureOptions(e.target));
    const textResponse = await response.text();
    const result = ParseTxt(textResponse);
    MessageBody.appendChild(result);

    const ul = result.querySelector(".chatBody .allMessagesList .listOfChats");


    result.addEventListener('submit', (e) => onSendMessage(e, chat, ul))

    chat.RecibedMessage(HandlerRecibeMessages);
};

ChatsIcon.forEach(icon => icon.addEventListener('submit', async (e) => await MakeFetch(e)));
