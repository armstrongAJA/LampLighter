using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataService//Interface to implement in JSONDataService script to add layer of abstraction
{
    bool SaveData<T>(string RelativePath, T Data, bool Encrypted);

    T LoadData<T>(string RelativePath, bool Encrypted);

}
