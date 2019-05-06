var MMessage = function (type, message) {

    function toString() {
        return message.toString();
    }

    return { "type": type, "message": message };
};

var MessageTypes = Object.freeze({
    "GAMEINFO": 0,
    "GAMECOMMAND": 1,
    "GAMECONNECT": 2
});