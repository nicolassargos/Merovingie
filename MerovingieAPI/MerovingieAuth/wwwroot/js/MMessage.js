var isNullOrUndefined = function (object) {
    return object === null || object === undefined;
}

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
};

function MGameFileDescriptor(gameDescriptor) {
    if (isNullOrUndefined(gameDescriptor.Carries)) {
        console.log('MGameFileDescriptor: carries are null or undefined');
        return;
    }
    if (isNullOrUndefined(gameDescriptor.Farms)) {
        console.log('MGameFileDescriptor: farms are null or undefined');
        return;
    }
    if (isNullOrUndefined(gameDescriptor.GoldMines)) {
        console.log('MGameFileDescriptor: gold mines are null or undefined');
        return;
    }
    if (isNullOrUndefined(gameDescriptor.Resources)
        || isNullOrUndefined(gameDescriptor.Resources.Gold)
        || isNullOrUndefined(gameDescriptor.Resources.Stone)
        || isNullOrUndefined(gameDescriptor.Resources.Wood)) {
        console.log('MGameFileDescriptor: at least one resource are null or undefined');
        return;
    }
    if (isNullOrUndefined(gameDescriptor.TownHalls)) {
        console.log('MGameFileDescriptor: town halls are null or undefined');
        return;
    }
    if (isNullOrUndefined(gameDescriptor.Trees)) {
        console.log('MGameFileDescriptor: trees are null or undefined');
        return;
    }
    if (isNullOrUndefined(gameDescriptor.Workers)) {
        console.log('MGameFileDescriptor: workers are null or undefined');
        return;
    }

    this.carries = gameDescriptor.Carries;
    this.farms = gameDescriptor.Farms;
    this.goldMines = gameDescriptor.GoldMines;
    this.resources = gameDescriptor.Resources;
    this.townHalls = gameDescriptor.TownHalls;
    this.trees = gameDescriptor.Trees;
    this.workers = gameDescriptor.Workers;

    return this;
};


var MessageTypes = Object.freeze({
    "GAMECONNECT_DEMAND":           0,
    "GAMECONNECT_OK":               1,
    "GAMECONNECT_ERROR":            2,
    "FILELOAD_REQUESTED":           3,
    "FILELOAD_ERROR_NOTFOUND":      4,
    "FILELOAD_ERROR_CORRUPTED":     5,
    "FILELOAD_ERROR_UNAUTHORIZED":  6,
    "FILELOAD_ACCEPTED":            7,
    "FILESAVE_REQUESTED_FIRSTPART": 8,
    "FILESAVE_REQUESTED_NEXTPART":  9,
    "FILESAVE_REQUESTED_END":       10,
    "FILESAVE_COMPLETED":           11,
    "FILESAVE_ERROR_CORRUPTED":     12,
    "FILESAVE_ERROR_UNAUTHORIZED":  13,
    "FILESAVE_ERROR_COMPLETED":     14,
    "CREATION_REQUESTED":           15,
    "CREATION_ACCEPTED":            16,
    "CREATION_ABORTED":             17,
    "CREATION_COMPLETED":           18,
    "CREATION_REFUSEDRESOURCES":    19,
    "CREATION_REFUSEDPOPULATION":   20,
    "CREATION_ERROR":               21,
    "CLIENTDATA_UNITSSTATE":        22,
    "INFO":                         23
    
});