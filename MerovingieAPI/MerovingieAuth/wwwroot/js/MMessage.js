export class MMessage {
    constructor(type, message) {
        this.type = type;
        this.message = message;
    }

    toString() {
        return message.toString();
    }
}

export const MessageTypes = Object.freeze({
    GAMEINFO: Symbol(0),
    GAMECOMMAND: Symbol(1)
});