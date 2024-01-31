using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public class JSONDataService : IDataService
{
    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted)//function to save the data
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
            Debug.LogError($"Data not saved due to: {e.Message} + {e.StackTrace}");
            return false;
        }
        
    }

    public T LoadData<T>(string RelativePath, bool Encrypted)//function to load the data
    {
        string path = Application.persistentDataPath + RelativePath;
        if(!File.Exists(path))
        {
            Debug.LogError($"File does not exist at {path}");
            throw new FileNotFoundException($"{path} does not exist!");
        }
        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to load data due to {e.Message} + {e.StackTrace}");
            throw e;
        }
    }
}
