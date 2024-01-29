using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMO : MonoBehaviour
{
    private IDataService DataService = new JSONDataService();
    private bool EncryptionEnabled;
    private PlayerData playerData;

    public void SerializeJson()
    {
        if (DataService.SaveData("/playerStats.Json", playerData, EncryptionEnabled))
        {
            
        }
        else
        {
            Debug.LogError("Could Not Save File!");

        }
    }
}
