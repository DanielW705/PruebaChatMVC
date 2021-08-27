"use strict"
/******Variable globales***** */
//Generamos nuestra conexion a nuestro Back end con el hub
const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
// Obtenemos lo que queremos observar si muta
const navbarCon = document.getElementById("lista-conectados");
const seccionMensajes = document.querySelector(".message-body");
//Este form es para los mensajes
const form = document.querySelector(".input-message");
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
    fetch(url,
        {
            method: "POST",
            headers: guardarHeaders(),
            body: idUser,
            redirect: "follow"
        }
    ).then(resposne => appendObject(resposne));
};
const agregarListener = (Node) => {
    Node.addEventListener("click", () => {
        FetchAPartialView(Node.getAttribute("data-idChat"));
    });
}
const limpiar = (select) => {
    for (let i = 0; i < select.options.length; i++) {
        console.log("borre informacion");
        select.remove(i);
    }
};
const iniciarLista = (lista) => {
    const select = document.getElementById("select-prueba");
    limpiar(select)
    const listaJson = JSON.parse(lista);
    listaJson.forEach(elemnto => {
        const option = document.createElement("option");
        option.textContent = elemnto.UserName;
        option.value = elemnto.idChat;
        select.appendChild(option);
    });
}
const imprimirMensajeEnviado = (message, date, sender) => {
    const lista = document.querySelector(".lista-mensajes");
    const enlistado = document.createElement("li");
    const div1 = document.createElement("div");
    div1.classList.add("message-section");
    const div2 = document.createElement("div");
    div2.classList.add("time-section");
    div1.innerHTML = `<span>${sender}</span> ${message}`;
    div2.innerText = date;
    enlistado.append(div1);
    enlistado.append(div2);
    enlistado.classList.add("message-send");
    lista.append(enlistado);
};
const imprimirMensajeRecibido = (message, date, sender) => {
    const lista = document.querySelector(".lista-mensajes");
    const enlistado = document.createElement("li");
    const div1 = document.createElement("div");
    div1.classList.add("message-section");
    const div2 = document.createElement("div");
    div2.classList.add("time-section");
    div1.innerHTML = `<span>${sender}</span> ${message}`;
    div2.innerText = date;
    enlistado.append(div1);
    enlistado.append(div2);
    enlistado.classList.add("message-recive");
    lista.append(enlistado);
};
const agregarListenerSendMesaje = (Node) => {
    const form = Node.querySelector(".input-message");
    const select = document.getElementById("select-prueba").value;
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        const message = Node.querySelector('input[type="text"]').value;
        if (!(message === "")) {
            const IdReceiver = Node.querySelector('input[type="hidden"]').value;
            connection.invoke("EnviarMensaje", message, select);
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
    //console.clear();
    //Obtenemos el nombre de usuario
    const nombreDeUsuario = document.getElementById("userName").value;
    //Iniciamos uso del patron observador, invokando a un metodo llamado "Usuario Conectado" con web sockets
    connection.invoke("UsuarioConectado", nombreDeUsuario);
});
/***************Este es el llenado del select*****************/
connection.on("RetornoDeID", lista => iniciarLista(lista));
/*********************Aqui generamos todos lo usuarios que se conectan************************/
connection.on("RetornoDeConectados", respuesta => ListCreation(respuesta));
/*****Recibimos lo que enviamos*********/
connection.on("MensajeMio", (respuesta, dateSend, sender) => imprimirMensajeEnviado(respuesta, dateSend, sender));
/*******Recibimos el mensaje********/
connection.on("MensajeRecibido", (respuesta, dateSend, sender) => imprimirMensajeRecibido(respuesta, dateSend, sender));
/**********Mutation events*********/
//observer.observe(navbarCon, observerOption)
/*observadorDeMensajes.observe(seccionMensajes, observerOption*/
/************Prueba*************/
form.addEventListener('submit', (e) => {
    e.preventDefault();
    const select = document.getElementById("select-prueba");
    const message = form.querySelector('input[type="text"]');
    const FechaDeEnvio = new Date;
    if (!(message === "")) {
        connection.invoke("EnviarMensaje", message.value, select.value, FechaDeEnvio.toLocaleString("es-MX"));
        message.value = "";
    }
});