using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject FrogEnemy;

    public PlayerData playerData;
    GameObject[] lamps;
    GameObject player;
    GameObject[] entranceList;
    ExitEntranceTrigger exitEntranceTrigger;//variable to hold exit class component of entrances gameobjects
    public float triggerDelayOnRoomEntrance = .5f;
    public Vector2 entranceSpeed = new Vector2(20, 0);
    private Rigidbody2D rb;
    private NewPlayerMovement movementScript;
    static int entranceIndex;
    public Transform lampSpawnPosition;

    // Start is called before the first frame update
    public static SpawnManager instance = null;
    private void Awake()
    {
        if (instance == null)//check if already an instance of spawn manager
        {
            instance = this;//if no instance, set instance to this span manager script
            DontDestroyOnLoad(base.gameObject);
        }
        else//if already one there, destroy this spawn manager game object
        {
            Destroy(base.gameObject);
        }
    }
    private void Start()
    {
        player = GameObject.FindWithTag("Player");//find the player object
        rb = player.GetComponent<Rigidbody2D>();
        movementScript = player.GetComponent<NewPlayerMovement>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindWithTag("Player");//find the player object
        rb = player.GetComponent<Rigidbody2D>();
        movementScript = player.GetComponent<NewPlayerMovement>();
        entranceList = GameObject.FindGameObjectsWithTag("Exit");//find all exits and append to a list

        Debug.Log("RunningOnSceneLoad");
        if (playerData.respawning)//reset respawning variable to false if the player died and its not a scene transition
        {
            InitializeLampsInLevel();
            foreach (GameObject lamp in lamps)//loop over all the lamps in the scene
            {

                if (lamp.GetComponent<Lamp>().lamp.lampIndex == playerData.lastLamp.lampIndex)//if the lamp is the current spawn point
                {
                    Debug.Log(lamp.GetComponent<Lamp>().lamp.lampIndex);
                    //yield return new WaitForSeconds(playerData.spawnWaitTime);
                    player.transform.position = lamp.transform.position;//spawn player at last lamp
                    Debug.Log("Reset player transform to lamp");
                    playerData.ResetStatsOnSpawn();//reset health etc on spawn
                                                   //playerData.dead = false;
                    Debug.Log("isPlayerRespawning = " + playerData.respawning);
                    playerData.respawning = false;
                    playerData.dead = false;
                    Debug.Log("Respawned at:" + player.transform.position);

                }
            }
        }
        else if (playerData.isExitingScene)
        {
            Debug.Log("Scene Loaded Because Exiting Scene Via Trigger" + entranceList + movementScript);
            foreach (GameObject entrance in entranceList)//loop over all the entrances in the list
            {
                exitEntranceTrigger = entrance.GetComponent<ExitEntranceTrigger>();//get the exit class component of each entrance
                if (exitEntranceTrigger.exit.exitIndex == entranceIndex)//only do this code for
                                                                        //the exit with index matching the new entrance
                {
                    Debug.Log("EntranceIndex:" + exitEntranceTrigger.exit.entranceIndex);
                    //exitEntranceTrigger.exit.exitCollider.enabled = false;//disable exit collider so you dont immediately switch levels again
                    //player.GetComponent<Rigidbody2D>().MovePosition(exitEntranceTrigger.exit.exitCollider.transform.position);//set the spawn position of the player
                    StartCoroutine(ControlTriggerBehaviour(triggerDelayOnRoomEntrance, exitEntranceTrigger.exit.exitCollider));
                    player.transform.position = exitEntranceTrigger.exit.exitCollider.transform.position;//set the spawn position of the player
                }
            }
        }
    }
    public void ExitScene(Exit exit)
    {
        //do the code to exit the scene, load new one and spawn at entrance based of entrance index
        entranceIndex = exit.entranceIndex;//save the entrance index for the next level
        if (exit.isLevelExit)//if the exit leads to a different level
        {
            SceneManager.LoadScene(exit.newLevelName);//load this level
            playerData.isExitingScene = false;
        }
        else//else load spawnpoint within current level
        {
            entranceList = GameObject.FindGameObjectsWithTag("Exit");//find all exits and append to a list
            foreach (GameObject entrance in entranceList)//loop over all the entrances in the list
            {
                exitEntranceTrigger = entrance.GetComponent<ExitEntranceTrigger>();//get the exit class component of each entrance
                if (exitEntranceTrigger.exit.exitIndex == entranceIndex)//only do this code for
                                                                        //the exit with index matching the new entrance
                {
                    //player.GetComponent<Rigidbody2D>().MovePosition(exitEntranceTrigger.exit.exitCollider.transform.position);//set the spawn position of the player
                    StartCoroutine(ControlTriggerBehaviour(triggerDelayOnRoomEntrance, exitEntranceTrigger.exit.exitCollider));
                    player.transform.position = exitEntranceTrigger.exit.exitCollider.transform.position;//move player to collider position
                    
                }
            }
        }
    }
    IEnumerator ControlTriggerBehaviour(float triggerDelayOnRoomEntrance, Collider2D trigger)//turn trigger off and on upon room entrance
    {
        trigger.enabled = false;//disable trigger briefly
        playerData.isMovingEnabled = false;//disable inputs in player movement script
        playerData.isSceneTransitionActive = true;//set scene transition to be active to enable the automoving when exiting door
        playerData.sceneTransitionMovementDirectionHorizontal = exitEntranceTrigger.exit.sceneTransitionMovementDirectionHorizontal;//set speed to exit if door horizontal
        playerData.sceneTransitionMovementDirectionVertical = exitEntranceTrigger.exit.sceneTransitionMovementDirectionVertical;//set jump speed to exit if vertical
        yield return new WaitForSeconds(triggerDelayOnRoomEntrance);//keep moving with inputs disabled for a set time
        playerData.sceneTransitionMovementDirectionVertical = 0;//reset y movement to zero so it is not added again
        playerData.isMovingEnabled = true;//enable moving again
        trigger.enabled = true;//reenable trigger on door for if you want to go back after scene transition
    }

    public static implicit operator GameObject(SpawnManager v)
    {
        throw new NotImplementedException();
    }

    public IEnumerator SpawnAtBench()
    {
        //this runs in tandem with onsceneloaded (sorry for terrible naming of function)
        Debug.Log("Respawning");
        if (1==1)//(playerData.lastLamp.lampSceneIndex != SceneManager.GetActiveScene().buildIndex)//if last lamp in different scene
        {
            yield return new WaitForSeconds(playerData.spawnWaitTime);
            SceneManager.LoadScene(playerData.lastLamp.lampSceneIndex);//load that scene
            Debug.Log("LoadedNewScene");
            
        }
        
    }
    public void InitializeLampsInLevel()
    {
        lamps = GameObject.FindGameObjectsWithTag("Lamp");//find all the lamps in the scene
        
        foreach (GameObject lamp in lamps)
        {
            lamp.GetComponent<Lamp>().lamp.lampSceneIndex = SceneManager.GetActiveScene().buildIndex;//update scene index of each lamp in level to this scene
            Debug.Log("Lamp Scene index set to " + lamp.GetComponent<Lamp>().lamp.lampSceneIndex);
        }
        Debug.Log("InitializedLamps");
    }
}
