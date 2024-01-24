using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()//function to start the game
    {
        SceneManager.LoadScene("Level1");//load the first level
    }
   public void Quit()//function to quit the game
   {
      Application.Quit();//quit the application
   }

   public void LevelSelect()//function to switch to level select screen
   {
       SceneManager.LoadScene("LevelSelectScreen");//switch to level select scene
   }

   public void NewGame()//Function to start new game
   {
       GetComponent<PlayerSaveData>().NewGame();//Call the function to start a new game from the PlayerSaveData script
   }
}
