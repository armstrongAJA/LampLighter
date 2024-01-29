using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerSaveData : MonoBehaviour
{
    public static bool newGame = false;//update newGame variable to reset save

    //Lamp stuff:
    public int lastLampIndex;
    public int lastLampSceneIndex;

    //unlocked abilities:
    public bool wallJumpActive = false;
    public bool doubleJumpActive = false;
    public bool dashActive = false;

    public void SavePlayerData()//Define function to save the data
    {
        SaveSystem.SavePlayer(this);//call the function from the save system script to save the player data
        Debug.Log("Game Saved, Last Lamp is: " + lastLampSceneIndex + lastLampIndex);
    }

    public void LoadPlayerData()//function to load the data
    {
        PlayerDataClass data = SaveSystem.LoadPlayer();//Load the player data as a PlayerDataClass type variable (defined in my class)

        Debug.Log("Game loaded, Last Lamp is: " + lastLampSceneIndex + lastLampIndex);
    }
    public void NewGame()
    {
        newGame = true;//set newgame to true to save defaults in class
        SavePlayerData();//save defaults
        LoadPlayerData();//Load defaults saved on prior line
        newGame = false;//reset newgame to ensure nothing is overwritten
    }
}
