using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public class JSONDataService : IDataService
{
    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;
        try
        {
            if (File.Exists(path))
            {
                Debug.Log("Data already exists. Deleting data and replacing with new file.");
                File.Delete(path);
                
            }
            else
            {
                Debug.Log("Creating file for the first time...");
            }
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(Data));
            return true;
        }

        catch (Exception e)
        {
            Debug.LogError("Data not saved due to:" + e.Message + e.StackTrace);
            return false;
        }
        
    }

    public T LoadData<T>(string RelativePath, bool Encrypted)
    {
        throw new System.NotImplementedException();
    }
}
