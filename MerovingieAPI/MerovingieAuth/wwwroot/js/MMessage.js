function MMessage(type, message) {

    this.type = type;
    this.message = message;

    return this;
};

function MCreationRequestBody(creator, productable) {

    this.creator = creator;
    this.productable = productable;

    return this;
}

var MessageTypes = Object.freeze({
    "GAMECONNECT_DEMAND":   0,
    "GAMECONNECT_OK":       1,
    "GAMECONNECT_ERROR":    2,
    "CREATION_REQUESTED":   3,
    "CREATION_ACCEPTED":    4,
    "CREATION_CANCELED":    5,
    "CREATION_FINISHED":    6,
    "CREATION_ERROR":       7,
    "INFO":                 8
});