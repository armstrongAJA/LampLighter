using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem//define static class for savesystem
{
    private static string path = System.IO.Path.Combine(Application.persistentDataPath, "PlayerSaveData.binary");//define path to save/load data
    
    public static void SavePlayer(PlayerSaveData playersavedata)//declare function to save player data
    {
        BinaryFormatter formatter = new BinaryFormatter();//declare binary formatter to format data as desired
        FileStream stream = new FileStream(path, FileMode.Create);//declare file stream in create mode to write data to file

        PlayerDataClass data = new PlayerDataClass(playersavedata);//Define data to save as a new PlayerDataClass
        
        formatter.Serialize(stream, data);//set formatter to save data using the filestream
        stream.Close();//close the stream (avoids many errors)
    }

    public static PlayerDataClass LoadPlayer()//define function to load data (returning a PlayerDataClass object)
    {
        if (File.Exists(path))//do this if the filepath exists
        {
            BinaryFormatter formatter = new BinaryFormatter();//declare binary formatter
            FileStream stream = new FileStream(path, FileMode.Open);//declare filestream in open mode, no need to write here

            PlayerDataClass data = formatter.Deserialize(stream) as PlayerDataClass;//Deformat data from binary file to PlayerDataClass object
            stream.Close();//Close the stream to avoid unwanted errors

            return data;//return PlayerDataClass object with saved data in it
        }
        else//if the path doesn't exist
        {
            Debug.LogError("Save file not found in " + path);//throw up error
            return null;//return nothing
        }
    }

}
