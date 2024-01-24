using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerDataClass//create a new data class to define variables to save and load
{
    //declare variables
    public int MaxLevel;
    public bool NewGame = PlayerSaveData.newGame;

    public PlayerDataClass(PlayerSaveData playerSaveData)//start class
    {
        if (!NewGame)//check if starting a new game
        {
            MaxLevel = PlayerSaveData.MaxLevel;//if not, set maxlevel to that of save file
        }
        else
        {
            MaxLevel = 1;//if new game, default maxlevel to 1
        }
    }
}
