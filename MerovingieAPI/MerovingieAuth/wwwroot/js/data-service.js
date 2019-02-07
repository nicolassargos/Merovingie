import { MMessage } from "./MMessage";

export class DataService {

    constructor() {
        this.scheme = document.location.protocol === "https:" ? "wss" : "ws";
        this.port = document.location.port ? (":" + document.location.port) : "";

        // Variable de connexion qui contient l'adresse du serveur
        this.connectionUrl = this.scheme + "://" + document.location.hostname + this.port + "/ws";

        // Connecte le websocket au serveur
        this.socket = new WebSocket(this.connectionUrl);
    }

    send(mMessage) {
        if (!this.socket || this.socket.readyState !== WebSocket.OPEN) {
            alert("socket not connected");
            return;
        }

        if (typeof (mMessage) !== 'MMessage') {
            alert('message to send is in a wrong format');
            return;
        }

        if (mMessage && mMessage.length > 0) {

            var jsonData = JSON.stringify(mMessage)

            socket.send(jsonData);
        }
    }

    open(event) {

    }
    
}

