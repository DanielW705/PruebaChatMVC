"use strict"
/******Variable globales***** */
//Generamos nuestra conexion a nuestro Back end con el hub
const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
// Obtenemos lo que queremos observar si muta
const navbarCon = document.getElementById("lista-conectados");
//Reglas de observacion
const observerOption = {
    attributes: true,
    childList: true,
    subtree: false,
};
const guardarHeaders = () => {
    let headers = new Headers()
    headers.append("Accept", "application/json");
    headers.append("Content-Type", "application/json");
    return headers;
}
/*******Metodos o funciones*******/
// Generamos una funcion que obtenga todo el objeto y lo vata agregando
const ListCreation = (StringResult) => {
    const jsonResult = JSON.parse(StringResult);
    const listaCon = document.getElementById("lista-conectados");
    listaCon.innerHTML = ' ';
    jsonResult.forEach(objeto => {
        const li = document.createElement("li");
        li.setAttribute("data-User", objeto.idUser);
        li.setAttribute("data-idChat", objeto.idChat);
        li.textContent = objeto.UserName;
        listaCon.appendChild(li);
    });
};
const FetchAPartialView = (idUser) => {
    const valor = JSON.stringify({
        "idUsuario": idUser
    });
    fetch(url,
        {
            method: "POST",
            headers: guardarHeaders(),
            body: valor,
            redirect: "follow"
        }
    ).then(resposne => resposne.text()).then(result => {
        const mostrar = document.querySelector(".message-body");
        const div = document.createElement("div");
        div.innerHTML = result;
        div.childNodes.forEach((child) => {
            mostrar.appendChild(child);
        });
    });
};
const agregarListener = (Node) => {
    Node.addEventListener("click", () => {
        FetchAPartialView(Node.getAttribute("data-User"));
    });
}

/********Objetos**********/
// Creamos nuestro objeto observador
const observer = new MutationObserver((mutationList) => {
    //retornara el elemento que a cambiado
    mutationList.forEach((mutations) => {
        mutations.addedNodes.forEach((Node) => agregarListener(Node));
    });
});
/*********WebSockets**********/
//Iniciamos la conexion al hub, para detener la conexion es el metodo stop(), si queremos saber que se desconecte "Aun no se"
connection.start().then(() => {
    //Obtenemos el nombre de usuario
    const nombreDeUsuario = document.getElementById("userName").value;
    //Iniciamos uso del patron observador, invokando a un metodo llamado "Usuario Conectado" con web sockets
    connection.invoke("UsuarioConectado", nombreDeUsuario);
});
connection.on("RetornoDeConectados", (respuesta) => ListCreation(respuesta));
/**********Mutation events*********/
observer.observe(navbarCon, observerOption)