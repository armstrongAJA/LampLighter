using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour
{
    //declare various variables
    private AudioSource finishSound;
    public static int CurrentLevel = 0;
    public Animator transition;
    public float transitionTime = 1;

    private bool LevelComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        finishSound = GetComponent<AudioSource>();//cache audiosource for use later

    }

    private void OnTriggerEnter2D(Collider2D collision)//when a collision happens
    {
        if (collision.gameObject.name == "Player" && !LevelComplete)//check if with the player and if the level is not already complete
        {
            finishSound.Play();//play the level finish audio
            LevelComplete = true;//set level to complete
            CompleteLevel();
        }
    }

    private void CompleteLevel()//method to complete the level abd change to next level
    {
        CurrentLevel = int.Parse(SceneManager.GetActiveScene().name[^1].ToString());//get current level as integer
        if (PlayerSaveData.MaxLevel <= CurrentLevel)//check if the max level reached in save is lower than the current level
        {
            Debug.Log("CurrentLevel" + CurrentLevel);
            PlayerSaveData.MaxLevel = CurrentLevel+1;//Increase Max level global variable
            Debug.Log("MaxLevel:" + PlayerSaveData.MaxLevel);
            GetComponent<PlayerSaveData>().SavePlayerData();//Save level
        }
        StartCoroutine(Loadlevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator Loadlevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}