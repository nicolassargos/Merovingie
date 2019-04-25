"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

connection.on("MessageInfo", function (message) {
    alert(message);
});

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});