using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerSaveData : MonoBehaviour
{
    public static int MaxLevel = 1 ;//MaxLevel is updated in the finish.cs script upon finishing a level to be max level reached so far
    public static bool newGame = false;//update newGame variable to reset save
    public void SavePlayerData()//Define function to save the data
    {
        SaveSystem.SavePlayer(this);//call the function from the save system script to save the player data
        Debug.Log("Game Saved, MaxLevel is: " + MaxLevel);
    }

    public void LoadPlayerData()//function to load the data
    {
        PlayerDataClass data = SaveSystem.LoadPlayer();//Load the player data as a PlayerDataClass type variable (defined in my class)
        MaxLevel = data.MaxLevel;//Set maxlevel to be that of the loaded data
        Debug.Log("Game loaded, MaxLevel is: " + MaxLevel);
    }
    public void NewGame()
    {
        newGame = true;//set newgame to true to save defaults in class
        SavePlayerData();//save defaults
        LoadPlayerData();//Load defaults saved on prior line
        newGame = false;//reset newgame to ensure nothing is overwritten
    }
}
