"use strict"
const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.start().then(() => {
    connection.invoke("UsuarioConectado", nombreUsuario);
});
connection.on("RetornoDeConectados", (respuesta) => {
    const listaCon = document.getElementById("lista-conectados");
    listaCon.innerHTML = " ";
    const lista = JSON.parse(respuesta);
    lista.forEach(objeto => {
        const li = document.createElement("li");
        li.setAttribute("data-User", objeto.idUser);
        li.setAttribute("data-idChat", objeto.idChat);
        li.textContent = objeto.UserName;
        listaCon.appendChild(li);
    });
});