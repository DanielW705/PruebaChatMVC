'use strict';
import { ChatHub } from "./Chat.js";
import { SendMessageDto } from "./SendMessageDto.js";
import { TypeOfMessage } from "./TypeOfMessage.js";

const chat = new ChatHub();

const chatBody = document.querySelector('.chatBody')

const ChatsIcon = chatBody.querySelectorAll(".chatsAvailableAside .listOfChats .chat .chatButton");

let ul = null;

const OnLoadWindow = (chatBody) => {
    const { userid } = chatBody.dataset;

    chat.StartConnection(userid);
}



OnLoadWindow(chatBody);
const FormatDate = (date) => {
    date = new Date(date);
    const año = date.getFullYear();
    const mes = String(date.getMonth() + 1).padStart(2, '0');
    const dia = String(date.getDate()).padStart(2, '0');
    const horas = String(date.getHours()).padStart(2, '0');
    const minutos = String(date.getMinutes()).padStart(2, '0');

    return `${año}-${mes}-${dia} ${horas}:${minutos}`;
};


const ConstructMessage = (data, type) => {

    console.log(data);

    const li = document.createElement("li");

    li.classList.add('rowMessage');

    const article = document.createElement("article");

    article.classList.add('message');

    const p = document.createElement('p');

    p.classList.add('textMessage');

    const time = document.createElement('time');

    time.classList.add('dateMessage');

    p.textContent = data.Message;

    time.textContent = FormatDate(data.Sended);

    time.dateTime = FormatDate(data.Sended);

    article.append(p);
    article.append(time);

    li.append(article);


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
    console.log(data);
    ul.appendChild(ConstructMessage(data, TypeOfMessage.Reciver))
}

const ParseTxt = (text) => {
    const parser = new DOMParser();

    let doc = parser.parseFromString(text, 'text/html');

    return doc.body.firstChild;
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
        ul.appendChild(ConstructMessage(sendMessageDto, TypeOfMessage.Sender));
    }

}



const MakeFetch = async (e) => {
    e.preventDefault();
    const MessageBody = document.querySelector(".messagesBody");
    const response = await fetch(e.target.action, ConfigureOptions(e.target));
    const textResponse = await response.text();
    const result = ParseTxt(textResponse);
    MessageBody.appendChild(result);

    ul = result.querySelector(".chatBody .allMessagesList .listOfChats");


    result.addEventListener('submit', (e) => onSendMessage(e, chat, ul))

    chat.RecibedMessage(HandlerRecibeMessages);
};

ChatsIcon.forEach(icon => icon.addEventListener('submit', async (e) => await MakeFetch(e)));
