using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerSaveData : MonoBehaviour
{
    public PlayerData playerData;
    IDataService DataService = new JSONDataService();
    
    public static bool newGame = false;//update newGame variable to reset save
    public bool EncryptionEnabled;

    //Lamp stuff:
    public int lastLampIndex;
    public int lastLampSceneIndex;

    //unlocked abilities:
    public bool wallJumpActive = false;
    public bool doubleJumpActive = false;
    public bool dashActive = false;

    //needs removing when I delete finish script and update levelloader script from starting scenes, but leave for now as it breaks the code
    public int MaxLevel = 1;
    private void Start()
    {
        
    }
    public void SavePlayerData()//Define function to save the data
    {
        //grab all data from the playerData Scriptable object you neeed to save:
        lastLampIndex = playerData.lastLamp.lampIndex;
        lastLampSceneIndex = playerData.lastLamp.lampSceneIndex;

        wallJumpActive = playerData.wallJumpActive;
        doubleJumpActive = playerData.doubleJumpActive;
        dashActive = playerData.dashActive;

        //Generate PlayerDataClass to save from this (because can't directly save a monobehaviour or scriptable object etc.)
        PlayerDataClass data = new PlayerDataClass();//Define data to save as a new PlayerDataClass
        data.PlayerDataClassInitializer(this);//initialize class (used method instead of constructor so it will also work for loading data
        SerializeJson(data);//save data
        Debug.Log("Game Saved, Last Lamp is: " + lastLampSceneIndex + lastLampIndex);
    }

    public void LoadPlayerData()//function to load the data
    {
        PlayerDataClass data = DeserializeJson();//Load the player data as a PlayerDataClass type variable (defined in my class)
        //send all data to scriptable object
        //load lamp data
        playerData.lastLamp.lampIndex = data.lastLampIndex;
        playerData.lastLamp.lampSceneIndex = data.lastLampSceneIndex;

        //load unlocked abilities
        playerData.wallJumpActive = data.wallJumpActive;
        playerData.doubleJumpActive = data.doubleJumpActive;
        playerData.dashActive = data.dashActive;
        Debug.Log("Game loaded, Last Lamp is: " + data.lastLampIndex + data.lastLampSceneIndex);
    }
    public void NewGame()//needs reworking
    {
        newGame = true;//set newgame to true to save defaults in class
        SavePlayerData();//save defaults
        LoadPlayerData();//Load defaults saved on prior line
        newGame = false;//reset newgame to ensure nothing is overwritten
    }
    public void SerializeJson(PlayerDataClass data)
    {
        if (DataService.SaveData("/playerStats.json", data, EncryptionEnabled))
        {
            Debug.Log("Saved File!");
        }
        else
        {
            Debug.LogError("Could Not Save File!");

        }
    }

    public PlayerDataClass DeserializeJson()
    {
        PlayerDataClass data = DataService.LoadData<PlayerDataClass>("/playerStats.json", EncryptionEnabled);
        return data;
    }
}
