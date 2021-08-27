"use strict"
/******Variable globales***** */
//Generamos nuestra conexion a nuestro Back end con el hub
const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
// Obtenemos lo que queremos observar si muta
const navbarCon = document.getElementById("lista-conectados");
const seccionMensajes = document.querySelector(".message-body");
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
const appendObject = async (response) => {
    const textoPlano = await response.text();
    const mostrar = document.querySelector(".message-body");
    mostrar.innerHTML = "";
    const div = document.createElement("div");
    div.innerHTML = textoPlano;
    div.childNodes.forEach((child) => {
        mostrar.appendChild(child);
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
    ).then(resposne => appendObject(resposne));
};
const agregarListener = (Node) => {
    Node.addEventListener("click", () => {
        FetchAPartialView(Node.getAttribute("data-idChat"));
    });
}
const agregarListenerSendMesaje = (Node) => {
    const form = Node.querySelector(".input-message");
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        const message = Node.querySelector('input[type="text"]').value;
        if (!(message === "")) {
            const IdReceiver = Node.querySelector('input[type="hidden"]').value;
            connection.invoke("EnviarMensaje", message, IdReceiver);
        }
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
const observadorDeMensajes = new MutationObserver(mutationList => agregarListenerSendMesaje(mutationList[0].target));
/*********WebSockets**********/
//Iniciamos la conexion al hub, para detener la conexion es el metodo stop(), si queremos saber que se desconecte "Aun no se"
connection.start().then(() => {
    //Obtenemos el nombre de usuario
    const nombreDeUsuario = document.getElementById("userName").value;
    //Iniciamos uso del patron observador, invokando a un metodo llamado "Usuario Conectado" con web sockets
    connection.invoke("UsuarioConectado", nombreDeUsuario);
});
/*******Recibimos el mensaje********/
connection.on("RetornoDeConectados", (respuesta) => ListCreation(respuesta));
connection.on("MensajeRecibido", (respuesta) => {
    //const lista = document.querySelector(".lista-mensajes");
    //const enlistado = document.createElement("li");
    //enlistado.classList.add("respuesta-mensaje");
    //enlistado.textContent = respuesta;
    //lista.append(enlistado);
})
/**********Mutation events*********/
observer.observe(navbarCon, observerOption)
observadorDeMensajes.observe(seccionMensajes, observerOption);