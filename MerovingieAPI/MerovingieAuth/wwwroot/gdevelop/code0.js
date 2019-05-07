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
gdjs.NewSceneCode.GDTownHallObjects1= [];
gdjs.NewSceneCode.GDTownHallObjects2= [];
gdjs.NewSceneCode.GDTownHallObjects3= [];
gdjs.NewSceneCode.GDwallObjects1= [];
gdjs.NewSceneCode.GDwallObjects2= [];
gdjs.NewSceneCode.GDwallObjects3= [];
gdjs.NewSceneCode.GDBuildButtonObjects1= [];
gdjs.NewSceneCode.GDBuildButtonObjects2= [];
gdjs.NewSceneCode.GDBuildButtonObjects3= [];

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


gdjs.NewSceneCode.userFunc0x723370 = function(runtimeScene, objects) {

    function MerovingieWebSocket() {
        this.scheme = document.location.protocol === "https:" ? "wss" : "ws";
        this.port = document.location.port ? (":" + document.location.port) : "";
        const urlParams = new URLSearchParams(window.location.search);
        var gameName = urlParams.get('name');

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

            var connectMessage = new MMessage(MessageTypes.GAMECONNECT_DEMAND, gameName);

            if (this.OPEN)
            {
                this.send(JSON.stringify(connectMessage));
            }
        }

        this.socket.onmessage = function (event) {
            var messageReceived = JSON.parse(event.data);
            console.log(messageReceived);

            var message = new MMessage(messageReceived.Type, messageReceived.Message);

            if (!message)
                console.error("Socket OnMessage: the format of the message is not correct :" + JSON.stringify(messageReceived));
            else
                gdjs.ProcessMessage(message);

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
        switch(messageToInterpret.type) {
            case MessageTypes.CREATION_COMPLETED:
                console.log(messageToInterpret.message);
                var messageToProcess = JSON.parse(messageToInterpret.message);
                gdjs.createWorker(null, messageToProcess.Position.x, messageToProcess.Position.y);
                break;
            default: break;
        }
        //console.log(JSON.stringify(messageToInterpret));
    }

    gdjs.createWorker = function(unit, x, y) {
        var newWorker = runtimeScene.createObject("worker");
        newWorker.setPosition(x, y);
        newWorker.setZOrder(1);
    }


    //(( gdjs.NewSceneCode.GDTownHallObjects1.length === 0 ) ? 0 :gdjs.NewSceneCode.GDTownHallObjects1[0].getPointX("Center")) + 200, (( gdjs.NewSceneCode.GDTownHallObjects1.length === 0 ) ? 0 :gdjs.NewSceneCode.GDTownHallObjects1[0].getPointY("Center")), "")



    
    //console.log(peasants);

};
gdjs.NewSceneCode.eventsList0x66f908 = function(runtimeScene) {

{


var objects = [];
gdjs.NewSceneCode.userFunc0x723370(runtimeScene, objects);

}


}; //End of gdjs.NewSceneCode.eventsList0x66f908
gdjs.NewSceneCode.eventsList0x736188 = function(runtimeScene) {

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
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7180164);
}
}}
if (gdjs.NewSceneCode.condition1IsTrue_0.val) {
/* Reuse gdjs.NewSceneCode.GDworkerObjects1 */
{for(var i = 0, len = gdjs.NewSceneCode.GDworkerObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDworkerObjects1[i].getBehavior("Pathfinding").moveTo(runtimeScene, gdjs.evtTools.input.getMouseX(runtimeScene, "", 0), gdjs.evtTools.input.getMouseY(runtimeScene, "", 0));
}
}}

}


}; //End of gdjs.NewSceneCode.eventsList0x736188
gdjs.NewSceneCode.eventsList0x6e7a28 = function(runtimeScene) {

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


}; //End of gdjs.NewSceneCode.eventsList0x6e7a28
gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDBuildButtonObjects1Objects = Hashtable.newFrom({"BuildButton": gdjs.NewSceneCode.GDBuildButtonObjects1});gdjs.NewSceneCode.userFunc0x7279b8 = function(runtimeScene, objects) {

var townHall = objects.find(x => x.getVariables().get("Selected").getAsNumber() === 1);
var townHallId = townHall.getNameId();

var messageBody = new MCreationRequestBody(townHallId, "worker", townHall.getX()+townHall.getWidth()+50,  townHall.getY()+ townHall.getHeight()+50);

gdjs.meroSocket.send(MessageTypes.CREATION_REQUESTED, messageBody);
};
gdjs.NewSceneCode.eventsList0x7448c8 = function(runtimeScene) {

{

/* Reuse gdjs.NewSceneCode.GDTownHallObjects1 */

var objects = [];
objects.push.apply(objects,gdjs.NewSceneCode.GDTownHallObjects1);
gdjs.NewSceneCode.userFunc0x7279b8(runtimeScene, objects);

}


}; //End of gdjs.NewSceneCode.eventsList0x7448c8
gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDworkerObjects2Objects = Hashtable.newFrom({"worker": gdjs.NewSceneCode.GDworkerObjects2});gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDTownHallObjects2Objects = Hashtable.newFrom({"TownHall": gdjs.NewSceneCode.GDTownHallObjects2});gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDworkerObjects2Objects = Hashtable.newFrom({"worker": gdjs.NewSceneCode.GDworkerObjects2});gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDTownHallObjects1Objects = Hashtable.newFrom({"TownHall": gdjs.NewSceneCode.GDTownHallObjects1});gdjs.NewSceneCode.eventsList0x675538 = function(runtimeScene) {

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
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(6903508);
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

gdjs.NewSceneCode.GDTownHallObjects2.createFrom(runtimeScene.getObjects("TownHall"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
gdjs.NewSceneCode.condition2IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.cursorOnObject(gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDTownHallObjects2Objects, runtimeScene, true, true);
}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDTownHallObjects2.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDTownHallObjects2[i].getVariableNumber(gdjs.NewSceneCode.GDTownHallObjects2[i].getVariables().getFromIndex(0)) == 1 ) {
        gdjs.NewSceneCode.condition1IsTrue_0.val = true;
        gdjs.NewSceneCode.GDTownHallObjects2[k] = gdjs.NewSceneCode.GDTownHallObjects2[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDTownHallObjects2.length = k;}if ( gdjs.NewSceneCode.condition1IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition2IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(6682372);
}
}}
}
if (gdjs.NewSceneCode.condition2IsTrue_0.val) {
gdjs.NewSceneCode.GDBuildButtonObjects2.createFrom(runtimeScene.getObjects("BuildButton"));
gdjs.NewSceneCode.GDItemBoardLabelObjects2.createFrom(runtimeScene.getObjects("ItemBoardLabel"));
gdjs.NewSceneCode.GDSelectedMarkerObjects2.createFrom(runtimeScene.getObjects("SelectedMarker"));
/* Reuse gdjs.NewSceneCode.GDTownHallObjects2 */
{for(var i = 0, len = gdjs.NewSceneCode.GDTownHallObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDTownHallObjects2[i].returnVariable(gdjs.NewSceneCode.GDTownHallObjects2[i].getVariables().getFromIndex(0)).setNumber(0);
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
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(6843020);
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

gdjs.NewSceneCode.GDTownHallObjects1.createFrom(runtimeScene.getObjects("TownHall"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
gdjs.NewSceneCode.condition2IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDTownHallObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDTownHallObjects1[i].getVariableNumber(gdjs.NewSceneCode.GDTownHallObjects1[i].getVariables().getFromIndex(0)) == 0 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDTownHallObjects1[k] = gdjs.NewSceneCode.GDTownHallObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDTownHallObjects1.length = k;}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
gdjs.NewSceneCode.condition1IsTrue_0.val = gdjs.evtTools.input.cursorOnObject(gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDTownHallObjects1Objects, runtimeScene, true, false);
}if ( gdjs.NewSceneCode.condition1IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition2IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(6826252);
}
}}
}
if (gdjs.NewSceneCode.condition2IsTrue_0.val) {
gdjs.NewSceneCode.GDBuildButtonObjects1.createFrom(runtimeScene.getObjects("BuildButton"));
gdjs.NewSceneCode.GDItemBoardLabelObjects1.createFrom(runtimeScene.getObjects("ItemBoardLabel"));
gdjs.NewSceneCode.GDSelectedMarkerObjects1.createFrom(runtimeScene.getObjects("SelectedMarker"));
/* Reuse gdjs.NewSceneCode.GDTownHallObjects1 */
{for(var i = 0, len = gdjs.NewSceneCode.GDTownHallObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDTownHallObjects1[i].returnVariable(gdjs.NewSceneCode.GDTownHallObjects1[i].getVariables().getFromIndex(0)).setNumber(1);
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


}; //End of gdjs.NewSceneCode.eventsList0x675538
gdjs.NewSceneCode.eventsList0xb2358 = function(runtimeScene) {

{


gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.runtimeScene.sceneJustBegins(runtimeScene);
}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {
gdjs.NewSceneCode.GDBuildButtonObjects1.createFrom(runtimeScene.getObjects("BuildButton"));
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
}
{ //Subevents
gdjs.NewSceneCode.eventsList0x66f908(runtimeScene);} //End of subevents
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
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(6853116);
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
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(6827780);
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
gdjs.NewSceneCode.eventsList0x736188(runtimeScene);} //End of subevents
}

}


{



}


{


{

{ //Subevents
gdjs.NewSceneCode.eventsList0x6e7a28(runtimeScene);} //End of subevents
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
gdjs.NewSceneCode.GDTownHallObjects1.createFrom(runtimeScene.getObjects("TownHall"));

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
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDTownHallObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDTownHallObjects1[i].getVariableNumber(gdjs.NewSceneCode.GDTownHallObjects1[i].getVariables().getFromIndex(0)) == 1 ) {
        gdjs.NewSceneCode.condition2IsTrue_0.val = true;
        gdjs.NewSceneCode.GDTownHallObjects1[k] = gdjs.NewSceneCode.GDTownHallObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDTownHallObjects1.length = k;}if ( gdjs.NewSceneCode.condition2IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition3IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7502156);
}
}}
}
}
if (gdjs.NewSceneCode.condition3IsTrue_0.val) {

{ //Subevents
gdjs.NewSceneCode.eventsList0x7448c8(runtimeScene);} //End of subevents
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
gdjs.NewSceneCode.eventsList0x675538(runtimeScene);} //End of subevents
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

gdjs.NewSceneCode.GDTownHallObjects1.createFrom(runtimeScene.getObjects("TownHall"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDTownHallObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDTownHallObjects1[i].getVariableNumber(gdjs.NewSceneCode.GDTownHallObjects1[i].getVariables().getFromIndex(0)) == 1 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDTownHallObjects1[k] = gdjs.NewSceneCode.GDTownHallObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDTownHallObjects1.length = k;}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {
gdjs.NewSceneCode.GDSelectedMarkerObjects1.createFrom(runtimeScene.getObjects("SelectedMarker"));
/* Reuse gdjs.NewSceneCode.GDTownHallObjects1 */
{for(var i = 0, len = gdjs.NewSceneCode.GDSelectedMarkerObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDSelectedMarkerObjects1[i].putAroundObject((gdjs.NewSceneCode.GDTownHallObjects1.length !== 0 ? gdjs.NewSceneCode.GDTownHallObjects1[0] : null), 0, 0);
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
gdjs.NewSceneCode.GDTownHallObjects1.length = 0;
gdjs.NewSceneCode.GDTownHallObjects2.length = 0;
gdjs.NewSceneCode.GDTownHallObjects3.length = 0;
gdjs.NewSceneCode.GDwallObjects1.length = 0;
gdjs.NewSceneCode.GDwallObjects2.length = 0;
gdjs.NewSceneCode.GDwallObjects3.length = 0;
gdjs.NewSceneCode.GDBuildButtonObjects1.length = 0;
gdjs.NewSceneCode.GDBuildButtonObjects2.length = 0;
gdjs.NewSceneCode.GDBuildButtonObjects3.length = 0;

gdjs.NewSceneCode.eventsList0xb2358(runtimeScene);
return;
}
gdjs['NewSceneCode'] = gdjs.NewSceneCode;
