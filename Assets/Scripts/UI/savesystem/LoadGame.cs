using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PlayerSaveData>().LoadPlayerData();//When the first scene is Loaded, the savedata is loaded
    }
}
