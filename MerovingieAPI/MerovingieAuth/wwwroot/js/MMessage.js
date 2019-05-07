function MMessage(type, message) {

    if (!type && type !== 0) console.log('McreationRequestBody: creator is null');
    if (!message) console.log('McreationRequestBody: productable is null');

    this.type = type;
    this.message = message;

    return this;
};

function MCreationRequestBody(creatorId, productableName, x, y) {

    if (!creatorId && creatorId !== 0) console.log('McreationRequestBody: creatorId is null');
    if (!productableName) console.log('McreationRequestBody: productableName is null');

    this.creatorId = creatorId;
    this.productableName = productableName;
    this.positionX = x;
    this.positionY = y;

    return this;
}


var MessageTypes = Object.freeze({
    "GAMECONNECT_DEMAND":   0,
    "GAMECONNECT_OK":       1,
    "GAMECONNECT_ERROR":    2,
    "CREATION_REQUESTED":   3,
    "CREATION_ACCEPTED":    4,
    "CREATION_ABORTED":     5,
    "CREATION_COMPLETED":   6,
    "CREATION_ERROR":       7,
    "INFO":                 8
});