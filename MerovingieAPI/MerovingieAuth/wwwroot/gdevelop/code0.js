gdjs.NewSceneCode = {};
gdjs.NewSceneCode.GDworkerObjects1= [];
gdjs.NewSceneCode.GDworkerObjects2= [];
gdjs.NewSceneCode.GDworkerObjects3= [];
gdjs.NewSceneCode.GDmessageObjects1= [];
gdjs.NewSceneCode.GDmessageObjects2= [];
gdjs.NewSceneCode.GDmessageObjects3= [];
gdjs.NewSceneCode.GDmessage2Objects1= [];
gdjs.NewSceneCode.GDmessage2Objects2= [];
gdjs.NewSceneCode.GDmessage2Objects3= [];
gdjs.NewSceneCode.GDItemBoardLabelObjects1= [];
gdjs.NewSceneCode.GDItemBoardLabelObjects2= [];
gdjs.NewSceneCode.GDItemBoardLabelObjects3= [];
gdjs.NewSceneCode.GDtreeiconObjects1= [];
gdjs.NewSceneCode.GDtreeiconObjects2= [];
gdjs.NewSceneCode.GDtreeiconObjects3= [];
gdjs.NewSceneCode.GDBackgroundObjects1= [];
gdjs.NewSceneCode.GDBackgroundObjects2= [];
gdjs.NewSceneCode.GDBackgroundObjects3= [];
gdjs.NewSceneCode.GDWoodPanelObjects1= [];
gdjs.NewSceneCode.GDWoodPanelObjects2= [];
gdjs.NewSceneCode.GDWoodPanelObjects3= [];
gdjs.NewSceneCode.GDSelectedMarkerObjects1= [];
gdjs.NewSceneCode.GDSelectedMarkerObjects2= [];
gdjs.NewSceneCode.GDSelectedMarkerObjects3= [];
gdjs.NewSceneCode.GDtownHallObjects1= [];
gdjs.NewSceneCode.GDtownHallObjects2= [];
gdjs.NewSceneCode.GDtownHallObjects3= [];
gdjs.NewSceneCode.GDwallObjects1= [];
gdjs.NewSceneCode.GDwallObjects2= [];
gdjs.NewSceneCode.GDwallObjects3= [];
gdjs.NewSceneCode.GDBuildButtonObjects1= [];
gdjs.NewSceneCode.GDBuildButtonObjects2= [];
gdjs.NewSceneCode.GDBuildButtonObjects3= [];
gdjs.NewSceneCode.GDGoldStockLabelObjects1= [];
gdjs.NewSceneCode.GDGoldStockLabelObjects2= [];
gdjs.NewSceneCode.GDGoldStockLabelObjects3= [];
gdjs.NewSceneCode.GDStoneStockLabelObjects1= [];
gdjs.NewSceneCode.GDStoneStockLabelObjects2= [];
gdjs.NewSceneCode.GDStoneStockLabelObjects3= [];
gdjs.NewSceneCode.GDWoodStockLabelObjects1= [];
gdjs.NewSceneCode.GDWoodStockLabelObjects2= [];
gdjs.NewSceneCode.GDWoodStockLabelObjects3= [];
gdjs.NewSceneCode.GDGoldStockIconObjects1= [];
gdjs.NewSceneCode.GDGoldStockIconObjects2= [];
gdjs.NewSceneCode.GDGoldStockIconObjects3= [];
gdjs.NewSceneCode.GDStoneStockIconObjects1= [];
gdjs.NewSceneCode.GDStoneStockIconObjects2= [];
gdjs.NewSceneCode.GDStoneStockIconObjects3= [];
gdjs.NewSceneCode.GDWoodStockIconObjects1= [];
gdjs.NewSceneCode.GDWoodStockIconObjects2= [];
gdjs.NewSceneCode.GDWoodStockIconObjects3= [];
gdjs.NewSceneCode.GDErrorLabelObjects1= [];
gdjs.NewSceneCode.GDErrorLabelObjects2= [];
gdjs.NewSceneCode.GDErrorLabelObjects3= [];
gdjs.NewSceneCode.GDcarryObjects1= [];
gdjs.NewSceneCode.GDcarryObjects2= [];
gdjs.NewSceneCode.GDcarryObjects3= [];
gdjs.NewSceneCode.GDmineObjects1= [];
gdjs.NewSceneCode.GDmineObjects2= [];
gdjs.NewSceneCode.GDmineObjects3= [];
gdjs.NewSceneCode.GDSaveButtonObjects1= [];
gdjs.NewSceneCode.GDSaveButtonObjects2= [];
gdjs.NewSceneCode.GDSaveButtonObjects3= [];

gdjs.NewSceneCode.conditionTrue_0 = {val:false};
gdjs.NewSceneCode.condition0IsTrue_0 = {val:false};
gdjs.NewSceneCode.condition1IsTrue_0 = {val:false};
gdjs.NewSceneCode.condition2IsTrue_0 = {val:false};
gdjs.NewSceneCode.condition3IsTrue_0 = {val:false};
gdjs.NewSceneCode.condition4IsTrue_0 = {val:false};
gdjs.NewSceneCode.conditionTrue_1 = {val:false};
gdjs.NewSceneCode.condition0IsTrue_1 = {val:false};
gdjs.NewSceneCode.condition1IsTrue_1 = {val:false};
gdjs.NewSceneCode.condition2IsTrue_1 = {val:false};
gdjs.NewSceneCode.condition3IsTrue_1 = {val:false};
gdjs.NewSceneCode.condition4IsTrue_1 = {val:false};


gdjs.NewSceneCode.userFunc0x7150b8 = function(runtimeScene, objects) {

    function MerovingieWebSocket() {
        this.scheme = document.location.protocol === "https:" ? "wss" : "ws";
        this.port = document.location.port ? (":" + document.location.port) : "";
        const urlParams = new URLSearchParams(window.location.search);
        this.gameName = urlParams.get('name');

        // Variable de connexion qui contient l'adresse du serveur
        this.connectionUrl = this.scheme + "://" + document.location.hostname + this.port + "/ws";

        // Connecte le websocket au serveur
        this.socket = new WebSocket(this.connectionUrl);

        // Ouverture du socket
        this.socket.onopen = function () {
            if (!this || this.readyState !== WebSocket.OPEN) {
                alert("meroSocket is not connected");
                return;
            }

            var connectMessage = new MMessage(MessageTypes.GAMECONNECT_DEMAND, gdjs.meroSocket.gameName);

            if (this.OPEN)
            {
                this.send(JSON.stringify(connectMessage));
            }
        }

        this.socket.onmessage = function (event) {
            var messageReceived = JSON.parse(event.data);
            console.log(messageReceived);

            if (!messageReceived.Type)
                console.error("Socket OnMessage: the format of the message is not correct :" + JSON.stringify(messageReceived));
            else
            {
                var message = new MMessage(messageReceived.Type, messageReceived.Message);
                gdjs.ProcessMessage(message);
            }

        }

        this.socket.onclose = function(event) {
            console.log(JSON.parse(event.data));
            // this.socket = new WebSocket(this.connectionUrl);
        }
    }

    // Méthode d'envoi de données
    MerovingieWebSocket.prototype.send = function(messageType, body) {

        if (!messageType) {
            alert('Message type is undefined of null');
            return;
        }

        if (!body) {
            alert('Body of message is undefined or null');
        }

        var message = new MMessage(messageType, body);

        this.socket.send(JSON.stringify(message));
    }

    gdjs.meroSocket = new MerovingieWebSocket();
    

    gdjs.ProcessMessage = function(messageToInterpret)
    {
        console.log(messageToInterpret.message);

        var messageBody = '';

        if (messageToInterpret.message && messageToInterpret.message.length > 0)
            messageBody = JSON.parse(messageToInterpret.message);

        switch(messageToInterpret.type) {

            case MessageTypes.GAMECONNECT_OK:
                // TODO: créer une fonction de chargement de partie
                // et l'attacher à meroSocket
                gdjs.meroSocket.send(MessageTypes.FILELOAD_REQUESTED, gdjs.meroSocket.gameName);
                break;

            case MessageTypes.FILELOAD_ACCEPTED:
                gdjs.loadData(messageBody);
                break;

            case MessageTypes.CREATION_REFUSEDPOPULATION:
                gdjs.displayError("Not enough farms!");
                break;

            case MessageTypes.CREATION_REFUSEDRESOURCES:
                gdjs.displayError("Not enough resources!");
                break;

            case MessageTypes.CREATION_ACCEPTED:
                gdjs.updateStock(messageBody);
                break;

            case MessageTypes.CREATION_COMPLETED:
                gdjs.createWorker(null, messageBody.Position.x, messageBody.Position.y);
                break;
            
            default: break;
        }
    }


    // Fonction qui traite de chargement de la partie
    gdjs.loadData = function(data) {
        console.log(data);
        var gameDescriptor = new MFileLoadDescriptor(data);
        // Tester la nullité et envoyer un message d'erreur au serveur si nécessaire
        // if (isNullOrUndefined(gameDescriptor)) gdjs.meroSocket.send(new MF)

        gameDescriptor.carries.forEach((carry) => gdjs.createCarry(carry));
        gameDescriptor.goldMines.forEach((mine) => gdjs.createGoldMine(mine));
        gameDescriptor.townHalls.forEach((townHall) => gdjs.createTownHall(townHall));
        gameDescriptor.workers.forEach((worker) => gdjs.createWorker(worker));
    }


    // Fonction de création d'une Carrière de pierre
    gdjs.createCarry = function(carry) {
        var newCarry = runtimeScene.createObject("carry");
        newCarry.setPosition(carry.Position.x, carry.Position.y);
        newCarry.setZOrder(1);
    }

    // Fonction de création d'une mine d'or'
    gdjs.createGoldMine = function(goldMine) {
        var newGoldMine = runtimeScene.createObject("mine");
        newGoldMine.setPosition(goldMine.Position.x, goldMine.Position.y);
        newGoldMine.setZOrder(1);
    }

    // Fonction de création d'une mine d'or'
    gdjs.createTownHall = function(townHall) {
        var newTownHall = runtimeScene.createObject("townHall");
        newTownHall.setPosition(townHall.Position.x, townHall.Position.y);
        newTownHall.setZOrder(1);
    }

    // Fonction de création d'un worker
    gdjs.createWorker = function(worker, x, y) {
        var newWorker = runtimeScene.createObject("worker");
        if (!worker || (worker.Position.x === 0 && worker.Position.y === 0))
        {
            newWorker.setPosition(x, y);            
        }
        else
        {
            newWorker.setPosition(worker.Position.x, worker.Position.y);
        }
        newWorker.setZOrder(1);
    }

    // Fonction de mise à jour du stock
    gdjs.updateStock = function(stock)
    {
        var goldStockLabel = runtimeScene.getObjects("GoldStockLabel");
        var stoneStockLabel = runtimeScene.getObjects("StoneStockLabel");
        var woodStockLabel = runtimeScene.getObjects("WoodStockLabel");

        if (goldStockLabel.length > 0) goldStockLabel[0].setString(stock.Gold);
        if (stoneStockLabel.length > 0) stoneStockLabel[0].setString(stock.Stone);
        if (woodStockLabel.length > 0) woodStockLabel[0].setString(stock.Wood);
    }

    // Fonction d'affichage d'erreur sur l'interface
    gdjs.displayError = function(errorMessage)
    {
        var errorLabel = runtimeScene.getObjects("ErrorLabel");
        errorLabel[0].setString(errorMessage);
        errorLabel[0].hide(false);
        setTimeout(function() {
            errorLabel[0].hide(true);
        }, 2000);
    }
    
    

};
gdjs.NewSceneCode.eventsList0x79c580 = function(runtimeScene) {

{


var objects = [];
gdjs.NewSceneCode.userFunc0x7150b8(runtimeScene, objects);

}


}; //End of gdjs.NewSceneCode.eventsList0x79c580
gdjs.NewSceneCode.eventsList0x767428 = function(runtimeScene) {

{

gdjs.NewSceneCode.GDworkerObjects1.createFrom(runtimeScene.getObjects("worker"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDworkerObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDworkerObjects1[i].getVariableNumber(gdjs.NewSceneCode.GDworkerObjects1[i].getVariables().getFromIndex(0)) == 1 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDworkerObjects1[k] = gdjs.NewSceneCode.GDworkerObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDworkerObjects1.length = k;}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition1IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(6924004);
}
}}
if (gdjs.NewSceneCode.condition1IsTrue_0.val) {
/* Reuse gdjs.NewSceneCode.GDworkerObjects1 */
{for(var i = 0, len = gdjs.NewSceneCode.GDworkerObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDworkerObjects1[i].getBehavior("Pathfinding").moveTo(runtimeScene, gdjs.evtTools.input.getMouseX(runtimeScene, "", 0), gdjs.evtTools.input.getMouseY(runtimeScene, "", 0));
}
}}

}


}; //End of gdjs.NewSceneCode.eventsList0x767428
gdjs.NewSceneCode.eventsList0x7026c8 = function(runtimeScene) {

{

gdjs.NewSceneCode.GDworkerObjects1.createFrom(runtimeScene.getObjects("worker"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDworkerObjects1.length;i<l;++i) {
    if ( !(gdjs.NewSceneCode.GDworkerObjects1[i].getBehavior("Pathfinding").pathFound()) ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDworkerObjects1[k] = gdjs.NewSceneCode.GDworkerObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDworkerObjects1.length = k;}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {
gdjs.NewSceneCode.GDmessageObjects1.createFrom(runtimeScene.getObjects("message"));
{for(var i = 0, len = gdjs.NewSceneCode.GDmessageObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDmessageObjects1[i].setString("I can't go there");
}
}}

}


}; //End of gdjs.NewSceneCode.eventsList0x7026c8
gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDBuildButtonObjects1Objects = Hashtable.newFrom({"BuildButton": gdjs.NewSceneCode.GDBuildButtonObjects1});gdjs.NewSceneCode.userFunc0x6ea670 = function(runtimeScene, objects) {

var townHall = runtimeScene.getObjects("townHall").find(x => x.getVariables().get("Selected").getAsNumber() === 1);
var townHallId = townHall.getNameId();

var messageBody = new MCreationRequestBody(townHallId, "worker", townHall.getX()+townHall.getWidth()+50,  townHall.getY()+ townHall.getHeight());

gdjs.meroSocket.send(MessageTypes.CREATION_REQUESTED, messageBody);
};
gdjs.NewSceneCode.eventsList0x65d048 = function(runtimeScene) {

{


var objects = [];
gdjs.NewSceneCode.userFunc0x6ea670(runtimeScene, objects);

}


}; //End of gdjs.NewSceneCode.eventsList0x65d048
gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDworkerObjects2Objects = Hashtable.newFrom({"worker": gdjs.NewSceneCode.GDworkerObjects2});gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDtownHallObjects2Objects = Hashtable.newFrom({"townHall": gdjs.NewSceneCode.GDtownHallObjects2});gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDworkerObjects2Objects = Hashtable.newFrom({"worker": gdjs.NewSceneCode.GDworkerObjects2});gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDtownHallObjects1Objects = Hashtable.newFrom({"townHall": gdjs.NewSceneCode.GDtownHallObjects1});gdjs.NewSceneCode.eventsList0x6f7520 = function(runtimeScene) {

{

gdjs.NewSceneCode.GDworkerObjects2.createFrom(runtimeScene.getObjects("worker"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
gdjs.NewSceneCode.condition2IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.cursorOnObject(gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDworkerObjects2Objects, runtimeScene, true, true);
}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDworkerObjects2.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDworkerObjects2[i].getVariableNumber(gdjs.NewSceneCode.GDworkerObjects2[i].getVariables().getFromIndex(0)) == 1 ) {
        gdjs.NewSceneCode.condition1IsTrue_0.val = true;
        gdjs.NewSceneCode.GDworkerObjects2[k] = gdjs.NewSceneCode.GDworkerObjects2[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDworkerObjects2.length = k;}if ( gdjs.NewSceneCode.condition1IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition2IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7251804);
}
}}
}
if (gdjs.NewSceneCode.condition2IsTrue_0.val) {
gdjs.NewSceneCode.GDItemBoardLabelObjects2.createFrom(runtimeScene.getObjects("ItemBoardLabel"));
gdjs.NewSceneCode.GDSelectedMarkerObjects2.createFrom(runtimeScene.getObjects("SelectedMarker"));
/* Reuse gdjs.NewSceneCode.GDworkerObjects2 */
{for(var i = 0, len = gdjs.NewSceneCode.GDworkerObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDworkerObjects2[i].returnVariable(gdjs.NewSceneCode.GDworkerObjects2[i].getVariables().getFromIndex(0)).setNumber(0);
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDSelectedMarkerObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDSelectedMarkerObjects2[i].hide();
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDItemBoardLabelObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDItemBoardLabelObjects2[i].hide();
}
}}

}


{

gdjs.NewSceneCode.GDtownHallObjects2.createFrom(runtimeScene.getObjects("townHall"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
gdjs.NewSceneCode.condition2IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.cursorOnObject(gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDtownHallObjects2Objects, runtimeScene, true, true);
}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDtownHallObjects2.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDtownHallObjects2[i].getVariableNumber(gdjs.NewSceneCode.GDtownHallObjects2[i].getVariables().getFromIndex(0)) == 1 ) {
        gdjs.NewSceneCode.condition1IsTrue_0.val = true;
        gdjs.NewSceneCode.GDtownHallObjects2[k] = gdjs.NewSceneCode.GDtownHallObjects2[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDtownHallObjects2.length = k;}if ( gdjs.NewSceneCode.condition1IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition2IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7325604);
}
}}
}
if (gdjs.NewSceneCode.condition2IsTrue_0.val) {
gdjs.NewSceneCode.GDBuildButtonObjects2.createFrom(runtimeScene.getObjects("BuildButton"));
gdjs.NewSceneCode.GDItemBoardLabelObjects2.createFrom(runtimeScene.getObjects("ItemBoardLabel"));
gdjs.NewSceneCode.GDSelectedMarkerObjects2.createFrom(runtimeScene.getObjects("SelectedMarker"));
/* Reuse gdjs.NewSceneCode.GDtownHallObjects2 */
{for(var i = 0, len = gdjs.NewSceneCode.GDtownHallObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDtownHallObjects2[i].returnVariable(gdjs.NewSceneCode.GDtownHallObjects2[i].getVariables().getFromIndex(0)).setNumber(0);
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDSelectedMarkerObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDSelectedMarkerObjects2[i].hide();
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDItemBoardLabelObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDItemBoardLabelObjects2[i].hide();
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDBuildButtonObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDBuildButtonObjects2[i].hide();
}
}}

}


{

gdjs.NewSceneCode.GDworkerObjects2.createFrom(runtimeScene.getObjects("worker"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
gdjs.NewSceneCode.condition2IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDworkerObjects2.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDworkerObjects2[i].getVariableNumber(gdjs.NewSceneCode.GDworkerObjects2[i].getVariables().getFromIndex(0)) == 0 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDworkerObjects2[k] = gdjs.NewSceneCode.GDworkerObjects2[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDworkerObjects2.length = k;}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
gdjs.NewSceneCode.condition1IsTrue_0.val = gdjs.evtTools.input.cursorOnObject(gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDworkerObjects2Objects, runtimeScene, true, false);
}if ( gdjs.NewSceneCode.condition1IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition2IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7762580);
}
}}
}
if (gdjs.NewSceneCode.condition2IsTrue_0.val) {
gdjs.NewSceneCode.GDItemBoardLabelObjects2.createFrom(runtimeScene.getObjects("ItemBoardLabel"));
gdjs.NewSceneCode.GDSelectedMarkerObjects2.createFrom(runtimeScene.getObjects("SelectedMarker"));
/* Reuse gdjs.NewSceneCode.GDworkerObjects2 */
{for(var i = 0, len = gdjs.NewSceneCode.GDworkerObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDworkerObjects2[i].returnVariable(gdjs.NewSceneCode.GDworkerObjects2[i].getVariables().getFromIndex(0)).setNumber(1);
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDSelectedMarkerObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDSelectedMarkerObjects2[i].hide(false);
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDItemBoardLabelObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDItemBoardLabelObjects2[i].hide(false);
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDItemBoardLabelObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDItemBoardLabelObjects2[i].setString("Peasant");
}
}{gdjs.evtTools.sound.playSound(runtimeScene, "sounds\\peasant-onclick01.mp3", false, 50, 1);
}}

}


{

gdjs.NewSceneCode.GDtownHallObjects1.createFrom(runtimeScene.getObjects("townHall"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
gdjs.NewSceneCode.condition2IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDtownHallObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDtownHallObjects1[i].getVariableNumber(gdjs.NewSceneCode.GDtownHallObjects1[i].getVariables().getFromIndex(0)) == 0 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDtownHallObjects1[k] = gdjs.NewSceneCode.GDtownHallObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDtownHallObjects1.length = k;}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
gdjs.NewSceneCode.condition1IsTrue_0.val = gdjs.evtTools.input.cursorOnObject(gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDtownHallObjects1Objects, runtimeScene, true, false);
}if ( gdjs.NewSceneCode.condition1IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition2IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7767324);
}
}}
}
if (gdjs.NewSceneCode.condition2IsTrue_0.val) {
gdjs.NewSceneCode.GDBuildButtonObjects1.createFrom(runtimeScene.getObjects("BuildButton"));
gdjs.NewSceneCode.GDItemBoardLabelObjects1.createFrom(runtimeScene.getObjects("ItemBoardLabel"));
gdjs.NewSceneCode.GDSelectedMarkerObjects1.createFrom(runtimeScene.getObjects("SelectedMarker"));
/* Reuse gdjs.NewSceneCode.GDtownHallObjects1 */
{for(var i = 0, len = gdjs.NewSceneCode.GDtownHallObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDtownHallObjects1[i].returnVariable(gdjs.NewSceneCode.GDtownHallObjects1[i].getVariables().getFromIndex(0)).setNumber(1);
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDSelectedMarkerObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDSelectedMarkerObjects1[i].hide(false);
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDItemBoardLabelObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDItemBoardLabelObjects1[i].setString("Town Hall");
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDItemBoardLabelObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDItemBoardLabelObjects1[i].hide(false);
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDBuildButtonObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDBuildButtonObjects1[i].hide(false);
}
}{gdjs.evtTools.sound.playSound(runtimeScene, "sounds\\townhall-onclick01.mp3", false, 50, 1);
}}

}


}; //End of gdjs.NewSceneCode.eventsList0x6f7520
gdjs.NewSceneCode.eventsList0xb2358 = function(runtimeScene) {

{


gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.runtimeScene.sceneJustBegins(runtimeScene);
}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {
gdjs.NewSceneCode.GDBuildButtonObjects1.createFrom(runtimeScene.getObjects("BuildButton"));
gdjs.NewSceneCode.GDErrorLabelObjects1.createFrom(runtimeScene.getObjects("ErrorLabel"));
gdjs.NewSceneCode.GDItemBoardLabelObjects1.createFrom(runtimeScene.getObjects("ItemBoardLabel"));
gdjs.NewSceneCode.GDSelectedMarkerObjects1.createFrom(runtimeScene.getObjects("SelectedMarker"));
gdjs.NewSceneCode.GDworkerObjects1.createFrom(runtimeScene.getObjects("worker"));
{gdjs.evtTools.camera.showLayer(runtimeScene, "Panel layer");
}{for(var i = 0, len = gdjs.NewSceneCode.GDItemBoardLabelObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDItemBoardLabelObjects1[i].hide();
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDSelectedMarkerObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDSelectedMarkerObjects1[i].hide();
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDworkerObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDworkerObjects1[i].setZOrder(1);
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDBuildButtonObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDBuildButtonObjects1[i].hide();
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDErrorLabelObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDErrorLabelObjects1[i].hide();
}
}
{ //Subevents
gdjs.NewSceneCode.eventsList0x79c580(runtimeScene);} //End of subevents
}

}


{



}


{

gdjs.NewSceneCode.GDworkerObjects1.createFrom(runtimeScene.getObjects("worker"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDworkerObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDworkerObjects1[i].getBehavior("Pathfinding").getSpeed() > 0 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDworkerObjects1[k] = gdjs.NewSceneCode.GDworkerObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDworkerObjects1.length = k;}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition1IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7339644);
}
}}
if (gdjs.NewSceneCode.condition1IsTrue_0.val) {
gdjs.NewSceneCode.GDmessageObjects1.createFrom(runtimeScene.getObjects("message"));
{for(var i = 0, len = gdjs.NewSceneCode.GDmessageObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDmessageObjects1[i].setString("I'm on my way");
}
}}

}


{



}


{

gdjs.NewSceneCode.GDworkerObjects1.createFrom(runtimeScene.getObjects("worker"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDworkerObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDworkerObjects1[i].getBehavior("Pathfinding").getSpeed() == 0 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDworkerObjects1[k] = gdjs.NewSceneCode.GDworkerObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDworkerObjects1.length = k;}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition1IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7980852);
}
}}
if (gdjs.NewSceneCode.condition1IsTrue_0.val) {
gdjs.NewSceneCode.GDmessageObjects1.createFrom(runtimeScene.getObjects("message"));
{for(var i = 0, len = gdjs.NewSceneCode.GDmessageObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDmessageObjects1[i].setString("Waiting for destination");
}
}}

}


{



}


{


gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.isMouseButtonReleased(runtimeScene, "Right");
}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {

{ //Subevents
gdjs.NewSceneCode.eventsList0x767428(runtimeScene);} //End of subevents
}

}


{



}


{


{

{ //Subevents
gdjs.NewSceneCode.eventsList0x7026c8(runtimeScene);} //End of subevents
}

}


{



}


{


{
gdjs.NewSceneCode.GDmessageObjects1.createFrom(runtimeScene.getObjects("message"));
gdjs.NewSceneCode.GDworkerObjects1.createFrom(runtimeScene.getObjects("worker"));
{for(var i = 0, len = gdjs.NewSceneCode.GDmessageObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDmessageObjects1[i].setPosition((( gdjs.NewSceneCode.GDworkerObjects1.length === 0 ) ? 0 :gdjs.NewSceneCode.GDworkerObjects1[0].getPointX("")) + 25,(( gdjs.NewSceneCode.GDworkerObjects1.length === 0 ) ? 0 :gdjs.NewSceneCode.GDworkerObjects1[0].getPointY("")) - 20);
}
}}

}


{



}


{


gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.isKeyPressed(runtimeScene, "Right");
}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {
{gdjs.evtTools.camera.setCameraX(runtimeScene, gdjs.evtTools.camera.getCameraX(runtimeScene, "", 0) + (10), "", 0);
}}

}


{


gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.isKeyPressed(runtimeScene, "Left");
}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {
{gdjs.evtTools.camera.setCameraX(runtimeScene, gdjs.evtTools.camera.getCameraX(runtimeScene, "", 0) - (10), "", 0);
}}

}


{


gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.isKeyPressed(runtimeScene, "Up");
}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {
{gdjs.evtTools.camera.setCameraY(runtimeScene, gdjs.evtTools.camera.getCameraY(runtimeScene, "", 0) - (10), "", 0);
}}

}


{


gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.isKeyPressed(runtimeScene, "Down");
}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {
{gdjs.evtTools.camera.setCameraY(runtimeScene, gdjs.evtTools.camera.getCameraY(runtimeScene, "", 0) + (10), "", 0);
}}

}


{

gdjs.NewSceneCode.GDBuildButtonObjects1.createFrom(runtimeScene.getObjects("BuildButton"));
gdjs.NewSceneCode.GDtownHallObjects1.createFrom(runtimeScene.getObjects("townHall"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
gdjs.NewSceneCode.condition2IsTrue_0.val = false;
gdjs.NewSceneCode.condition3IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.isMouseButtonPressed(runtimeScene, "Left");
}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
gdjs.NewSceneCode.condition1IsTrue_0.val = gdjs.evtTools.input.cursorOnObject(gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDBuildButtonObjects1Objects, runtimeScene, true, false);
}if ( gdjs.NewSceneCode.condition1IsTrue_0.val ) {
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDtownHallObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDtownHallObjects1[i].getVariableNumber(gdjs.NewSceneCode.GDtownHallObjects1[i].getVariables().getFromIndex(0)) == 1 ) {
        gdjs.NewSceneCode.condition2IsTrue_0.val = true;
        gdjs.NewSceneCode.GDtownHallObjects1[k] = gdjs.NewSceneCode.GDtownHallObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDtownHallObjects1.length = k;}if ( gdjs.NewSceneCode.condition2IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition3IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7251460);
}
}}
}
}
if (gdjs.NewSceneCode.condition3IsTrue_0.val) {

{ //Subevents
gdjs.NewSceneCode.eventsList0x65d048(runtimeScene);} //End of subevents
}

}


{



}


{


gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.isMouseButtonPressed(runtimeScene, "Left");
}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {

{ //Subevents
gdjs.NewSceneCode.eventsList0x6f7520(runtimeScene);} //End of subevents
}

}


{

gdjs.NewSceneCode.GDworkerObjects1.createFrom(runtimeScene.getObjects("worker"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDworkerObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDworkerObjects1[i].getVariableNumber(gdjs.NewSceneCode.GDworkerObjects1[i].getVariables().getFromIndex(0)) == 1 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDworkerObjects1[k] = gdjs.NewSceneCode.GDworkerObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDworkerObjects1.length = k;}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {
gdjs.NewSceneCode.GDSelectedMarkerObjects1.createFrom(runtimeScene.getObjects("SelectedMarker"));
/* Reuse gdjs.NewSceneCode.GDworkerObjects1 */
{for(var i = 0, len = gdjs.NewSceneCode.GDSelectedMarkerObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDSelectedMarkerObjects1[i].putAroundObject((gdjs.NewSceneCode.GDworkerObjects1.length !== 0 ? gdjs.NewSceneCode.GDworkerObjects1[0] : null), 0, 0);
}
}}

}


{

gdjs.NewSceneCode.GDtownHallObjects1.createFrom(runtimeScene.getObjects("townHall"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDtownHallObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDtownHallObjects1[i].getVariableNumber(gdjs.NewSceneCode.GDtownHallObjects1[i].getVariables().getFromIndex(0)) == 1 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDtownHallObjects1[k] = gdjs.NewSceneCode.GDtownHallObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDtownHallObjects1.length = k;}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {
gdjs.NewSceneCode.GDSelectedMarkerObjects1.createFrom(runtimeScene.getObjects("SelectedMarker"));
/* Reuse gdjs.NewSceneCode.GDtownHallObjects1 */
{for(var i = 0, len = gdjs.NewSceneCode.GDSelectedMarkerObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDSelectedMarkerObjects1[i].putAroundObject((gdjs.NewSceneCode.GDtownHallObjects1.length !== 0 ? gdjs.NewSceneCode.GDtownHallObjects1[0] : null), 0, 0);
}
}}

}


{


{
}

}


}; //End of gdjs.NewSceneCode.eventsList0xb2358


gdjs.NewSceneCode.func = function(runtimeScene) {
runtimeScene.getOnceTriggers().startNewFrame();
gdjs.NewSceneCode.GDworkerObjects1.length = 0;
gdjs.NewSceneCode.GDworkerObjects2.length = 0;
gdjs.NewSceneCode.GDworkerObjects3.length = 0;
gdjs.NewSceneCode.GDmessageObjects1.length = 0;
gdjs.NewSceneCode.GDmessageObjects2.length = 0;
gdjs.NewSceneCode.GDmessageObjects3.length = 0;
gdjs.NewSceneCode.GDmessage2Objects1.length = 0;
gdjs.NewSceneCode.GDmessage2Objects2.length = 0;
gdjs.NewSceneCode.GDmessage2Objects3.length = 0;
gdjs.NewSceneCode.GDItemBoardLabelObjects1.length = 0;
gdjs.NewSceneCode.GDItemBoardLabelObjects2.length = 0;
gdjs.NewSceneCode.GDItemBoardLabelObjects3.length = 0;
gdjs.NewSceneCode.GDtreeiconObjects1.length = 0;
gdjs.NewSceneCode.GDtreeiconObjects2.length = 0;
gdjs.NewSceneCode.GDtreeiconObjects3.length = 0;
gdjs.NewSceneCode.GDBackgroundObjects1.length = 0;
gdjs.NewSceneCode.GDBackgroundObjects2.length = 0;
gdjs.NewSceneCode.GDBackgroundObjects3.length = 0;
gdjs.NewSceneCode.GDWoodPanelObjects1.length = 0;
gdjs.NewSceneCode.GDWoodPanelObjects2.length = 0;
gdjs.NewSceneCode.GDWoodPanelObjects3.length = 0;
gdjs.NewSceneCode.GDSelectedMarkerObjects1.length = 0;
gdjs.NewSceneCode.GDSelectedMarkerObjects2.length = 0;
gdjs.NewSceneCode.GDSelectedMarkerObjects3.length = 0;
gdjs.NewSceneCode.GDtownHallObjects1.length = 0;
gdjs.NewSceneCode.GDtownHallObjects2.length = 0;
gdjs.NewSceneCode.GDtownHallObjects3.length = 0;
gdjs.NewSceneCode.GDwallObjects1.length = 0;
gdjs.NewSceneCode.GDwallObjects2.length = 0;
gdjs.NewSceneCode.GDwallObjects3.length = 0;
gdjs.NewSceneCode.GDBuildButtonObjects1.length = 0;
gdjs.NewSceneCode.GDBuildButtonObjects2.length = 0;
gdjs.NewSceneCode.GDBuildButtonObjects3.length = 0;
gdjs.NewSceneCode.GDGoldStockLabelObjects1.length = 0;
gdjs.NewSceneCode.GDGoldStockLabelObjects2.length = 0;
gdjs.NewSceneCode.GDGoldStockLabelObjects3.length = 0;
gdjs.NewSceneCode.GDStoneStockLabelObjects1.length = 0;
gdjs.NewSceneCode.GDStoneStockLabelObjects2.length = 0;
gdjs.NewSceneCode.GDStoneStockLabelObjects3.length = 0;
gdjs.NewSceneCode.GDWoodStockLabelObjects1.length = 0;
gdjs.NewSceneCode.GDWoodStockLabelObjects2.length = 0;
gdjs.NewSceneCode.GDWoodStockLabelObjects3.length = 0;
gdjs.NewSceneCode.GDGoldStockIconObjects1.length = 0;
gdjs.NewSceneCode.GDGoldStockIconObjects2.length = 0;
gdjs.NewSceneCode.GDGoldStockIconObjects3.length = 0;
gdjs.NewSceneCode.GDStoneStockIconObjects1.length = 0;
gdjs.NewSceneCode.GDStoneStockIconObjects2.length = 0;
gdjs.NewSceneCode.GDStoneStockIconObjects3.length = 0;
gdjs.NewSceneCode.GDWoodStockIconObjects1.length = 0;
gdjs.NewSceneCode.GDWoodStockIconObjects2.length = 0;
gdjs.NewSceneCode.GDWoodStockIconObjects3.length = 0;
gdjs.NewSceneCode.GDErrorLabelObjects1.length = 0;
gdjs.NewSceneCode.GDErrorLabelObjects2.length = 0;
gdjs.NewSceneCode.GDErrorLabelObjects3.length = 0;
gdjs.NewSceneCode.GDcarryObjects1.length = 0;
gdjs.NewSceneCode.GDcarryObjects2.length = 0;
gdjs.NewSceneCode.GDcarryObjects3.length = 0;
gdjs.NewSceneCode.GDmineObjects1.length = 0;
gdjs.NewSceneCode.GDmineObjects2.length = 0;
gdjs.NewSceneCode.GDmineObjects3.length = 0;
gdjs.NewSceneCode.GDSaveButtonObjects1.length = 0;
gdjs.NewSceneCode.GDSaveButtonObjects2.length = 0;
gdjs.NewSceneCode.GDSaveButtonObjects3.length = 0;

gdjs.NewSceneCode.eventsList0xb2358(runtimeScene);
return;
}
gdjs['NewSceneCode'] = gdjs.NewSceneCode;
