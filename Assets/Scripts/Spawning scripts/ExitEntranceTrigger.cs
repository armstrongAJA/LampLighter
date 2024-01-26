using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitEntranceTrigger : MonoBehaviour
{
    public Exit exit;
    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        exit.exitCollider = GetComponent<Collider2D>();
    }
    public void TriggerExitScene()
    {
        FindObjectOfType<SpawnManager>().ExitScene(exit);//call the exit scene method from the spawn manager
        Debug.Log("ExitingScene");
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))//if player enters exit trigger
        {
            TriggerExitScene();//trigger scene exit/entrance in new scene at right location
        }
    }
    //on level load:
    //get entrance index and move player to this entrance transform
    //load new exits:entrances in level from a variable (housed in a game object in each level)
    //on exit trigger:
    
}

