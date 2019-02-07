gdjs.NewSceneCode = {};
gdjs.NewSceneCode.GDpeasantObjects1= [];
gdjs.NewSceneCode.GDpeasantObjects2= [];
gdjs.NewSceneCode.GDpeasantObjects3= [];
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


gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDpeasantObjects1Objects = Hashtable.newFrom({"peasant": gdjs.NewSceneCode.GDpeasantObjects1});gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDpeasantObjects1Objects = Hashtable.newFrom({"peasant": gdjs.NewSceneCode.GDpeasantObjects1});gdjs.NewSceneCode.eventsList0x6e36e8 = function(runtimeScene) {

{



}


{


{
}

}


}; //End of gdjs.NewSceneCode.eventsList0x6e36e8
gdjs.NewSceneCode.eventsList0x6e3ca8 = function(runtimeScene) {

{



}


{


{
}

}


}; //End of gdjs.NewSceneCode.eventsList0x6e3ca8
gdjs.NewSceneCode.eventsList0x6e42e8 = function(runtimeScene) {

{

gdjs.NewSceneCode.GDpeasantObjects1.createFrom(runtimeScene.getObjects("peasant"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDpeasantObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDpeasantObjects1[i].getVariableNumber(gdjs.NewSceneCode.GDpeasantObjects1[i].getVariables().getFromIndex(0)) == 1 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDpeasantObjects1[k] = gdjs.NewSceneCode.GDpeasantObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDpeasantObjects1.length = k;}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition1IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7226612);
}
}}
if (gdjs.NewSceneCode.condition1IsTrue_0.val) {
/* Reuse gdjs.NewSceneCode.GDpeasantObjects1 */
{for(var i = 0, len = gdjs.NewSceneCode.GDpeasantObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDpeasantObjects1[i].getBehavior("Pathfinding").moveTo(runtimeScene, gdjs.evtTools.input.getMouseX(runtimeScene, "", 0), gdjs.evtTools.input.getMouseY(runtimeScene, "", 0));
}
}}

}


}; //End of gdjs.NewSceneCode.eventsList0x6e42e8
gdjs.NewSceneCode.eventsList0x6e4840 = function(runtimeScene) {

{

gdjs.NewSceneCode.GDpeasantObjects1.createFrom(runtimeScene.getObjects("peasant"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDpeasantObjects1.length;i<l;++i) {
    if ( !(gdjs.NewSceneCode.GDpeasantObjects1[i].getBehavior("Pathfinding").pathFound()) ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDpeasantObjects1[k] = gdjs.NewSceneCode.GDpeasantObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDpeasantObjects1.length = k;}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {
gdjs.NewSceneCode.GDmessageObjects1.createFrom(runtimeScene.getObjects("message"));
{for(var i = 0, len = gdjs.NewSceneCode.GDmessageObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDmessageObjects1[i].setString("I can't go there");
}
}}

}


}; //End of gdjs.NewSceneCode.eventsList0x6e4840
gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDBuildButtonObjects1Objects = Hashtable.newFrom({"BuildButton": gdjs.NewSceneCode.GDBuildButtonObjects1});gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDpeasantObjects1Objects = Hashtable.newFrom({"peasant": gdjs.NewSceneCode.GDpeasantObjects1});gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDpeasantObjects2Objects = Hashtable.newFrom({"peasant": gdjs.NewSceneCode.GDpeasantObjects2});gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDTownHallObjects2Objects = Hashtable.newFrom({"TownHall": gdjs.NewSceneCode.GDTownHallObjects2});gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDpeasantObjects2Objects = Hashtable.newFrom({"peasant": gdjs.NewSceneCode.GDpeasantObjects2});gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDTownHallObjects2Objects = Hashtable.newFrom({"TownHall": gdjs.NewSceneCode.GDTownHallObjects2});gdjs.NewSceneCode.eventsList0x6e5c10 = function(runtimeScene) {

{

gdjs.NewSceneCode.GDpeasantObjects2.createFrom(runtimeScene.getObjects("peasant"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
gdjs.NewSceneCode.condition2IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.cursorOnObject(gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDpeasantObjects2Objects, runtimeScene, true, true);
}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDpeasantObjects2.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDpeasantObjects2[i].getVariableNumber(gdjs.NewSceneCode.GDpeasantObjects2[i].getVariables().getFromIndex(0)) == 1 ) {
        gdjs.NewSceneCode.condition1IsTrue_0.val = true;
        gdjs.NewSceneCode.GDpeasantObjects2[k] = gdjs.NewSceneCode.GDpeasantObjects2[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDpeasantObjects2.length = k;}if ( gdjs.NewSceneCode.condition1IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition2IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7233276);
}
}}
}
if (gdjs.NewSceneCode.condition2IsTrue_0.val) {
gdjs.NewSceneCode.GDItemBoardLabelObjects2.createFrom(runtimeScene.getObjects("ItemBoardLabel"));
gdjs.NewSceneCode.GDSelectedMarkerObjects2.createFrom(runtimeScene.getObjects("SelectedMarker"));
/* Reuse gdjs.NewSceneCode.GDpeasantObjects2 */
{for(var i = 0, len = gdjs.NewSceneCode.GDpeasantObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDpeasantObjects2[i].returnVariable(gdjs.NewSceneCode.GDpeasantObjects2[i].getVariables().getFromIndex(0)).setNumber(0);
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
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7234420);
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

gdjs.NewSceneCode.GDpeasantObjects2.createFrom(runtimeScene.getObjects("peasant"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
gdjs.NewSceneCode.condition2IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDpeasantObjects2.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDpeasantObjects2[i].getVariableNumber(gdjs.NewSceneCode.GDpeasantObjects2[i].getVariables().getFromIndex(0)) == 0 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDpeasantObjects2[k] = gdjs.NewSceneCode.GDpeasantObjects2[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDpeasantObjects2.length = k;}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
gdjs.NewSceneCode.condition1IsTrue_0.val = gdjs.evtTools.input.cursorOnObject(gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDpeasantObjects2Objects, runtimeScene, true, false);
}if ( gdjs.NewSceneCode.condition1IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition2IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7235716);
}
}}
}
if (gdjs.NewSceneCode.condition2IsTrue_0.val) {
gdjs.NewSceneCode.GDItemBoardLabelObjects2.createFrom(runtimeScene.getObjects("ItemBoardLabel"));
gdjs.NewSceneCode.GDSelectedMarkerObjects2.createFrom(runtimeScene.getObjects("SelectedMarker"));
/* Reuse gdjs.NewSceneCode.GDpeasantObjects2 */
{for(var i = 0, len = gdjs.NewSceneCode.GDpeasantObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDpeasantObjects2[i].returnVariable(gdjs.NewSceneCode.GDpeasantObjects2[i].getVariables().getFromIndex(0)).setNumber(1);
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

gdjs.NewSceneCode.GDTownHallObjects2.createFrom(runtimeScene.getObjects("TownHall"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
gdjs.NewSceneCode.condition2IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDTownHallObjects2.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDTownHallObjects2[i].getVariableNumber(gdjs.NewSceneCode.GDTownHallObjects2[i].getVariables().getFromIndex(0)) == 0 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDTownHallObjects2[k] = gdjs.NewSceneCode.GDTownHallObjects2[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDTownHallObjects2.length = k;}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
gdjs.NewSceneCode.condition1IsTrue_0.val = gdjs.evtTools.input.cursorOnObject(gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDTownHallObjects2Objects, runtimeScene, true, false);
}if ( gdjs.NewSceneCode.condition1IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition2IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7237348);
}
}}
}
if (gdjs.NewSceneCode.condition2IsTrue_0.val) {
gdjs.NewSceneCode.GDBuildButtonObjects2.createFrom(runtimeScene.getObjects("BuildButton"));
gdjs.NewSceneCode.GDItemBoardLabelObjects2.createFrom(runtimeScene.getObjects("ItemBoardLabel"));
gdjs.NewSceneCode.GDSelectedMarkerObjects2.createFrom(runtimeScene.getObjects("SelectedMarker"));
/* Reuse gdjs.NewSceneCode.GDTownHallObjects2 */
{for(var i = 0, len = gdjs.NewSceneCode.GDTownHallObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDTownHallObjects2[i].returnVariable(gdjs.NewSceneCode.GDTownHallObjects2[i].getVariables().getFromIndex(0)).setNumber(1);
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDSelectedMarkerObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDSelectedMarkerObjects2[i].hide(false);
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDItemBoardLabelObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDItemBoardLabelObjects2[i].setString("Town Hall");
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDItemBoardLabelObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDItemBoardLabelObjects2[i].hide(false);
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDBuildButtonObjects2.length ;i < len;++i) {
    gdjs.NewSceneCode.GDBuildButtonObjects2[i].hide(false);
}
}{gdjs.evtTools.sound.playSound(runtimeScene, "sounds\\townhall-onclick01.mp3", false, 50, 1);
}}

}


{


{
}

}


}; //End of gdjs.NewSceneCode.eventsList0x6e5c10
gdjs.NewSceneCode.eventsList0xb0cf8 = function(runtimeScene) {

{


gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.runtimeScene.sceneJustBegins(runtimeScene);
}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {
gdjs.NewSceneCode.GDBuildButtonObjects1.createFrom(runtimeScene.getObjects("BuildButton"));
gdjs.NewSceneCode.GDItemBoardLabelObjects1.createFrom(runtimeScene.getObjects("ItemBoardLabel"));
gdjs.NewSceneCode.GDSelectedMarkerObjects1.createFrom(runtimeScene.getObjects("SelectedMarker"));
gdjs.NewSceneCode.GDpeasantObjects1.length = 0;

{gdjs.evtTools.camera.showLayer(runtimeScene, "Panel layer");
}{for(var i = 0, len = gdjs.NewSceneCode.GDItemBoardLabelObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDItemBoardLabelObjects1[i].hide();
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDSelectedMarkerObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDSelectedMarkerObjects1[i].hide();
}
}{gdjs.evtTools.object.createObjectOnScene((typeof eventsFunctionContext !== 'undefined' ? eventsFunctionContext : runtimeScene), gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDpeasantObjects1Objects, 570, 500, "");
}{gdjs.evtTools.object.createObjectOnScene((typeof eventsFunctionContext !== 'undefined' ? eventsFunctionContext : runtimeScene), gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDpeasantObjects1Objects, 800, 550, "");
}{for(var i = 0, len = gdjs.NewSceneCode.GDpeasantObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDpeasantObjects1[i].setZOrder(1);
}
}{for(var i = 0, len = gdjs.NewSceneCode.GDBuildButtonObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDBuildButtonObjects1[i].hide();
}
}}

}


{



}


{

gdjs.NewSceneCode.GDpeasantObjects1.createFrom(runtimeScene.getObjects("peasant"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDpeasantObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDpeasantObjects1[i].getBehavior("Pathfinding").getSpeed() > 0 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDpeasantObjects1[k] = gdjs.NewSceneCode.GDpeasantObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDpeasantObjects1.length = k;}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition1IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7223284);
}
}}
if (gdjs.NewSceneCode.condition1IsTrue_0.val) {
gdjs.NewSceneCode.GDmessageObjects1.createFrom(runtimeScene.getObjects("message"));
{for(var i = 0, len = gdjs.NewSceneCode.GDmessageObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDmessageObjects1[i].setString("I'm on my way");
}
}
{ //Subevents
gdjs.NewSceneCode.eventsList0x6e36e8(runtimeScene);} //End of subevents
}

}


{



}


{

gdjs.NewSceneCode.GDpeasantObjects1.createFrom(runtimeScene.getObjects("peasant"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
gdjs.NewSceneCode.condition1IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDpeasantObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDpeasantObjects1[i].getBehavior("Pathfinding").getSpeed() == 0 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDpeasantObjects1[k] = gdjs.NewSceneCode.GDpeasantObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDpeasantObjects1.length = k;}if ( gdjs.NewSceneCode.condition0IsTrue_0.val ) {
{
{gdjs.NewSceneCode.conditionTrue_1 = gdjs.NewSceneCode.condition1IsTrue_0;
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7224796);
}
}}
if (gdjs.NewSceneCode.condition1IsTrue_0.val) {
gdjs.NewSceneCode.GDmessageObjects1.createFrom(runtimeScene.getObjects("message"));
{for(var i = 0, len = gdjs.NewSceneCode.GDmessageObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDmessageObjects1[i].setString("Waiting for destination");
}
}
{ //Subevents
gdjs.NewSceneCode.eventsList0x6e3ca8(runtimeScene);} //End of subevents
}

}


{



}


{


gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.isMouseButtonReleased(runtimeScene, "Right");
}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {

{ //Subevents
gdjs.NewSceneCode.eventsList0x6e42e8(runtimeScene);} //End of subevents
}

}


{



}


{


gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.isMouseButtonReleased(runtimeScene, "Right");
}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {

{ //Subevents
gdjs.NewSceneCode.eventsList0x6e4840(runtimeScene);} //End of subevents
}

}


{



}


{


{
gdjs.NewSceneCode.GDmessageObjects1.createFrom(runtimeScene.getObjects("message"));
gdjs.NewSceneCode.GDpeasantObjects1.createFrom(runtimeScene.getObjects("peasant"));
{for(var i = 0, len = gdjs.NewSceneCode.GDmessageObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDmessageObjects1[i].setPosition((( gdjs.NewSceneCode.GDpeasantObjects1.length === 0 ) ? 0 :gdjs.NewSceneCode.GDpeasantObjects1[0].getPointX("")) + 25,(( gdjs.NewSceneCode.GDpeasantObjects1.length === 0 ) ? 0 :gdjs.NewSceneCode.GDpeasantObjects1[0].getPointY("")) - 20);
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
gdjs.NewSceneCode.conditionTrue_1.val = runtimeScene.getOnceTriggers().triggerOnce(7231764);
}
}}
}
}
if (gdjs.NewSceneCode.condition3IsTrue_0.val) {
/* Reuse gdjs.NewSceneCode.GDTownHallObjects1 */
gdjs.NewSceneCode.GDpeasantObjects1.length = 0;

{gdjs.evtTools.object.createObjectOnScene((typeof eventsFunctionContext !== 'undefined' ? eventsFunctionContext : runtimeScene), gdjs.NewSceneCode.mapOfGDgdjs_46NewSceneCode_46GDpeasantObjects1Objects, (( gdjs.NewSceneCode.GDTownHallObjects1.length === 0 ) ? 0 :gdjs.NewSceneCode.GDTownHallObjects1[0].getPointX("Center")), (( gdjs.NewSceneCode.GDTownHallObjects1.length === 0 ) ? 0 :gdjs.NewSceneCode.GDTownHallObjects1[0].getPointY("Center"))-60, "");
}{for(var i = 0, len = gdjs.NewSceneCode.GDpeasantObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDpeasantObjects1[i].setZOrder(1);
}
}}

}


{



}


{


gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
gdjs.NewSceneCode.condition0IsTrue_0.val = gdjs.evtTools.input.isMouseButtonPressed(runtimeScene, "Left");
}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {

{ //Subevents
gdjs.NewSceneCode.eventsList0x6e5c10(runtimeScene);} //End of subevents
}

}


{

gdjs.NewSceneCode.GDpeasantObjects1.createFrom(runtimeScene.getObjects("peasant"));

gdjs.NewSceneCode.condition0IsTrue_0.val = false;
{
for(var i = 0, k = 0, l = gdjs.NewSceneCode.GDpeasantObjects1.length;i<l;++i) {
    if ( gdjs.NewSceneCode.GDpeasantObjects1[i].getVariableNumber(gdjs.NewSceneCode.GDpeasantObjects1[i].getVariables().getFromIndex(0)) == 1 ) {
        gdjs.NewSceneCode.condition0IsTrue_0.val = true;
        gdjs.NewSceneCode.GDpeasantObjects1[k] = gdjs.NewSceneCode.GDpeasantObjects1[i];
        ++k;
    }
}
gdjs.NewSceneCode.GDpeasantObjects1.length = k;}if (gdjs.NewSceneCode.condition0IsTrue_0.val) {
gdjs.NewSceneCode.GDSelectedMarkerObjects1.createFrom(runtimeScene.getObjects("SelectedMarker"));
/* Reuse gdjs.NewSceneCode.GDpeasantObjects1 */
{for(var i = 0, len = gdjs.NewSceneCode.GDSelectedMarkerObjects1.length ;i < len;++i) {
    gdjs.NewSceneCode.GDSelectedMarkerObjects1[i].putAroundObject((gdjs.NewSceneCode.GDpeasantObjects1.length !== 0 ? gdjs.NewSceneCode.GDpeasantObjects1[0] : null), 0, 0);
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


}; //End of gdjs.NewSceneCode.eventsList0xb0cf8


gdjs.NewSceneCode.func = function(runtimeScene) {
runtimeScene.getOnceTriggers().startNewFrame();
gdjs.NewSceneCode.GDpeasantObjects1.length = 0;
gdjs.NewSceneCode.GDpeasantObjects2.length = 0;
gdjs.NewSceneCode.GDpeasantObjects3.length = 0;
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

gdjs.NewSceneCode.eventsList0xb0cf8(runtimeScene);
return;
}
gdjs['NewSceneCode'] = gdjs.NewSceneCode;
