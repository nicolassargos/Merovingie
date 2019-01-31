import { MMessage } from './MMessage.js';
import { DataService } from './data-service.js';




var connection = document.getElementById("connectionForm");
var connectionUrl = document.getElementById("connectionUrl");
var connectButton = document.getElementById("connectButton");
var stateLabel = document.getElementById("stateLabel");
var sendMessage = document.getElementById("sendMessage");
var sendButton = document.getElementById("sendButton");
var sendForm = document.getElementById("sendForm");
var closeButton = document.getElementById("closeButton");
var commsLog = document.getElementById("commsLog");

var loadbutton = document.getElementById("loadbutton");

// Message qui sera envoyé au serveur-
var mMessage = new MMessage();

// Initialise le service de Websocket
var dataService = new DataService();


// Mets à jour l'interface selon l'état du Websocket
//function updateState() {
//    function disable() {
//        sendMessage.disabled = true;
//        sendButton.disabled = true;
//        closeButton.disabled = true;
//    }
//    function enable() {
//        sendMessage.disabled = false;
//        sendButton.disabled = false;
//        closeButton.disabled = false;
//    }

//    connectionUrl.disabled = true;
//    connectButton.disabled = true;

//    if (!socket) { disable(); }
//    else {
//        switch (socket.readyState) {
//            case WebSocket.CLOSED:
//                stateLabel.innerHTML = "Closed";
//                disable();
//                connectionUrl.disabled = false;
//                connectButton.disabled = false;
//                break;
//            case WebSocket.CLOSING:
//                stateLabel.innerHTML = "Closing...";
//                disable();
//                break;
//            case WebSocket.CONNECTING:
//                stateLabel.innerHTML = "Connecting...";
//                disable();
//                break;
//            case WebSocket.OPEN:
//                stateLabel.innerHTML = "Open";
//                enable();
//                break;
//            default:
//                stateLabel.innerHTML = "Unknown state...:" + htmlEscape(socket.readyState);
//                disable();
//                break;
//        }
//    }
//}

////
////
////
//closeButton.onclick = function () {
//    if (!socket || socket.readyState !== WebSocket.OPEN) {
//        alert("socket not connected");
//    }
//    socket.close(1000, "closing from client");
//}

////
////
////
//sendButton.onclick = function () {
//    if (!socket || socket.readyState !== WebSocket.OPEN) {
//        alert("socket not connected");
//    }
//    if (sendMessage.value && sendMessage.value.length > 0) {
//        mMessage.type = 'worker';
//        mMessage.message = sendMessage.value;

//        var jsonData = JSON.stringify(mMessage)

//        socket.send(jsonData);

//        commsLog.innerHTML += '<tr>' +
//            '<td>Client</td>' +
//            '<td>Server</td>' +
//            '<td>' + htmlEscape(mMessage.message) + '</td>'
//            + '</tr>';
//    }
//}

////
////
////
//connectButton.onclick = function () {
//    stateLabel.innerHTML = "connection.....";
    

//    // 
//    socket.onopen = function (e) {
//        updateState();
//        commsLog.innerHTML += '<tr><td colspan="3"> Connection opened </td></tr>';
//    };

//    // 
//    socket.onclose = function (event) {
//        updateState();
//        commsLog.innerHTML += '<tr><td colspan="3"> Connection closed. Code:' +
//            htmlEscape(event.code) + '. Reason: ' + htmlEscape(event.reason) + '. </td ></tr > ';
//    };

//    //
//    socket.onerror = updateState();

//    //
//    socket.onmessage = function (event) {

//        var messageReceived = JSON.parse(event.data);

//        commsLog.innerHTML += '<tr>' +
//            '<td>Server</td>' +
//            '<td>Client</td>' +
//            '<td>' + htmlEscape(messageReceived.Message) + '</td>'
//            + '</tr>';
//        console.log(messageReceived.Message);
//    };


//}

//function htmlEscape(str) {
//    return str
//        .replace(/&/g, '&amp;')
//        .replace(/"/g, '&quot;')
//        .replace(/'/g, '&#39;')
//        .replace(/</g, '&lt;')
//        .replace(/>/g, '&gt;');
//}


////////
connectionUrl.value = dataService.connectionUrl;
alert('Websocket connecté');