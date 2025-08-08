'use strict';
(() => {
    const ParseTxt = (text) => {
        const parser = new DOMParser();

        let doc = parser.parseFromString(text, 'text/html');

        return doc.body;
    }
    const CreateBody = (data) => ({
        method: 'POST',
        body: new FormData(data),
        redirect: "follow"
    });
    const MakeFetch = async (e) => {
        e.preventDefault();
        const MessageBody = document.querySelector(".MessagesBody");
        const response = await fetch(e.target.action, CreateBody(e.target));
        const textResponse = await response.text();
        const result = ParseTxt(textResponse);
        MessageBody.appendChild(result);
    };
    const ChatsIcon = document.querySelectorAll(".ChatBody .ChatsAvailableAside .ListOfChats .Chat form");
    ChatsIcon.forEach(icon => icon.addEventListener('submit', async (e) => await MakeFetch(e)));
})()