using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    //Declare variables
    [SerializeField] private string LoadLevel;//level which needs to be loaded
    private void Start()
    {
        if (PlayerSaveData.MaxLevel < int.Parse(LoadLevel[^1].ToString()))//Check if level to be loaded in button is less than maximum unlocked
        {
            gameObject.SetActive(false);//if so, deactivate the button
        }
        else
        {
            gameObject.SetActive(true);//if not, activate the button
        }
    }
    
    public void PressLevelButton()//What to do if button is pressed
    {
        SceneManager.LoadScene(LoadLevel);//Load the level specified in LoadLevel variable
    }
}