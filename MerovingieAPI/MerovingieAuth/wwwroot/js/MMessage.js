var MMessage = function (type, message) {

    function toString() {
        return message.toString();
    }

    return { "type": type, "message": message };
};

var MessageTypes = Object.freeze({
    GAMEINFO: Symbol(0),
    GAMECOMMAND: Symbol(1),
    GAMECONNECT: Symbol(2)
});