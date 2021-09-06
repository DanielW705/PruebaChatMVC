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
    headers.append("Content-Type", "multipart/form-data");
    return headers;
}
/*******Metodos o funciones*******/
// Generamos una funcion que obtenga todo el objeto y lo vata agregando
const ListCreation = (StringResult) => {
    const jsonResult = JSON.parse(StringResult);
    const listaCon = document.getElementById("lista-conectados");
    listaCon.innerHTML = ' ';
    const idUsuario = document.getElementById("idUser").value;
    jsonResult.forEach(objeto => {
        if (objeto.idUser != idUsuario) {
            const li = document.createElement("li");
            li.setAttribute("data-User", objeto.idUser);
            li.setAttribute("data-idChat", objeto.idChat);
            li.textContent = objeto.UserName;
            listaCon.appendChild(li);
        }
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
const values = (idChat, idReciber, idSender) => {
    const Datos = new FormData();
    Datos.append("idChat", idChat);
    Datos.append("idReciber", idReciber);
    Datos.append("idSender", idSender);
    return Datos;
}
const FetchAPartialView = (idChat, idReciber, idSender) => {
    fetch(url,
        {
            method: "POST",
            //headers: guardarHeaders(),
            body: values(idChat, idReciber, idSender),
            redirect: "follow"
        }
    ).then(resposne => appendObject(resposne));
};
const agregarListener = (Node) => {
    Node.addEventListener("click", () => {
        const idReciber = Node.getAttribute("data-User");
        const idSender = document.getElementById("idUser").value;
        const idChat = Node.getAttribute("data-idChat");
        const bublee = Node.querySelector(".notificado-mensaje");
        if (bublee !== null) {
            Node.removeChild(bublee);
            connection.on("VistoRealizado", idChat)
        }
        FetchAPartialView(idChat, idReciber, idSender);
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
const imprimirMensajeRecibido = (message, date, sender, idEnviante) => {
    const lista = document.querySelector(".lista-mensajes");
    if (lista !== null) {
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
    }
    else {
        const conectados = document.getElementById("lista-conectados").querySelectorAll("li");
        conectados.forEach((connectado) => {
            if (connectado.attributes.getNamedItem("data-idchat").value === idEnviante) {
                const bublee = document.querySelector(".notificado-mensaje");
                if (bublee !== null) {
                    const contenido = Number.parseInt(bublee.textContent);
                    bublee.textContent = contenido + 1;
                }
                else {
                    const div = document.createElement("div");
                    div.classList.add("notificado-mensaje");
                    div.textContent = "1";
                    connectado.appendChild(div);
                }
            }
        });
    }
};
const agregarListenerSendMesaje = (Node) => {
    const form = Node.querySelector(".input-message");
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        const message = Node.querySelector('input[type="text"]');
        const IdReceiver = Node.querySelector('input[type="hidden"]').value;
        const FechaDeEnvio = new Date;
        if (!(message === "")) {
            connection.invoke("EnviarMensaje", message.value, IdReceiver, FechaDeEnvio.toLocaleString("es-MX"));
            message.value = "";
        }
    });
    const closeButton = Node.querySelector(".close");
    closeButton.addEventListener("click", () => {
        Node.innerHTML = "";
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
    const nombreDeUsuario = document.getElementById("userName").textContent;
    const idUsuario = document.getElementById("idUser").value;
    //Iniciamos uso del patron observador, invokando a un metodo llamado "Usuario Conectado" con web sockets
    connection.invoke("UsuarioConectado", idUsuario, nombreDeUsuario);
});
/***************Este es el llenado del select*****************/
//connection.on("RetornoDeID", lista => iniciarLista(lista));
/*********************Aqui generamos todos lo usuarios que se conectan************************/
connection.on("RetornoDeConectados", respuesta => ListCreation(respuesta));
/*****Recibimos lo que enviamos*********/
connection.on("MensajeMio", (respuesta, dateSend, sender) => imprimirMensajeEnviado(respuesta, dateSend, sender));
/*******Recibimos el mensaje********/
connection.on("MensajeRecibido", (respuesta, dateSend, sender, idEnviante) => imprimirMensajeRecibido(respuesta, dateSend, sender, idEnviante));
/**********Mutation events*********/
observer.observe(navbarCon, observerOption)
observadorDeMensajes.observe(seccionMensajes, observerOption);
///************Prueba*************/
////form.addEventListener('submit', (e) => {
////    e.preventDefault();
////    const select = document.getElementById("select-prueba");
////    const message = form.querySelector('input[type="text"]');
////    const FechaDeEnvio = new Date;
////    if (!(message === "")) {
////        connection.invoke("EnviarMensaje", message.value, select.value, FechaDeEnvio.toLocaleString("es-MX"));
////        message.value = "";
////    }
//});