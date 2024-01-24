using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //declare variables
    public static bool GameIsPaused = false;
    [SerializeField] private string MenuSceneName = "Menu";

    public GameObject PauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//check if pause key (esc) is pressed
        {
            if (GameIsPaused)//check if game is already paused
            {
                Resume();//Function to resume game
            }
            else//if not paused
            {
                Pause();//Function to pause game
            }
        }
    }

    public void Resume()//function to resume game
    {
        PauseMenuUI.SetActive(false);//set pause menu to inactive
        Time.timeScale = 1f;//reset timescale to normal
        GameIsPaused = false;//Set Game state to not be paused
    }

    private void Pause()//function to pause game
    {
        PauseMenuUI.SetActive(true);//Set pause menu to be active
        Time.timeScale = 0f;//discable time passing in game
        GameIsPaused = true;//Set Game state to be paused
    }

    public void LoadMenu()//function to load the menu
    {
        Debug.Log("LoadingMenu...");
        SceneManager.LoadScene(MenuSceneName);//Load the Menu scene
        Time.timeScale = 1f;//Set time to run as normal in game again
        GameIsPaused = false;//set game state to not be paused
    }

    public void QuitGame()//function to quit the game
    {
        Debug.Log("QuittingGame");
        Application.Quit();//Quit the application
    }
}
