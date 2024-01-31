using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    public BoxCollider2D coll;
    public PlayerSaveData playerSaveData;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)//on trigger, save the scriptable object as a json file
    {
        playerSaveData.SavePlayerData();
    }
}
