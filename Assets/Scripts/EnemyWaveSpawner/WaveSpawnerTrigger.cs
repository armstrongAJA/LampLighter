using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerTrigger : MonoBehaviour
{
    private WaveSpawner spawner;
    private Collider2D coll;

    private void Start()
    {
        spawner = GetComponent<WaveSpawner>();
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        spawner.battleOver = false;//set battle to not started yet
        spawner.battleTriggered = true;//trigger battle on collision with trigger
        spawner.enemiesBeforeWave = GameObject.FindGameObjectsWithTag("Enemy").Length;//set number of enemies to reach before spawning new wave
        coll.enabled = false;

    }
}
