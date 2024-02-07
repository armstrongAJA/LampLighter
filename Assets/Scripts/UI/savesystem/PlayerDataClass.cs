using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerDataClass//create a new data type to define variables to save and load
{
    //define variables you wish to save:

    //Lamp stuff:
    public int lastLampIndex;
    public int lastLampSceneIndex;

    //unlocked abilities:
    public bool wallJumpActive = false;
    public bool doubleJumpActive = false;
    public bool dashActive = false;

    public void PlayerDataClassInitializer(PlayerSaveData playerSaveData)//make a method to set these variables - this is called a constructor, to write the above class
    {
        lastLampIndex = playerSaveData.lastLampIndex;
        lastLampSceneIndex = playerSaveData.lastLampSceneIndex;
        wallJumpActive = playerSaveData.wallJumpActive;
        doubleJumpActive = playerSaveData.doubleJumpActive;
        dashActive = playerSaveData.dashActive;
    }
}
