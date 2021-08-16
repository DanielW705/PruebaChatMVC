"use strict"
const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.start().then(() => {
    console.log("Me conecte");
});