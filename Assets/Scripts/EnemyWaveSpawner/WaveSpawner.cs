using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]

public class Wave
{
    public string waveName;
    public int numberOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}
public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    private Wave currentWave;
    private int currentWaveNumber;
    private bool canSpawn = true;
    private float nextSpawnTime;
    public bool battleOver = false;
    public bool battleTriggered = false;
    public int enemiesBeforeWave;

    private void Update()
    {
        if (battleTriggered)
        {
            currentWave = waves[currentWaveNumber];//update current wave number
            SpawnWave();//implement code to spawn enemies within the current wave
            GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");//update total enemy counter
            if (totalEnemies.Length == enemiesBeforeWave && !canSpawn && currentWaveNumber + 1 != waves.Length)//if no enemies left, wave is over, not yet completed all waves
            {
                SpawnNextWave();//go to the next wave
            }
            else if (currentWaveNumber + 1 == waves.Length &&
                     totalEnemies.Length == enemiesBeforeWave && !canSpawn) //if all waves spawned and no enemies left
            {
                battleOver = true;//end battle (can use this as a trigger to open door etc.
            }
        }
        
    }

    private void SpawnWave()//code to spawn enemies in a wave
    {
        if (canSpawn && nextSpawnTime<Time.time)//if spawn timer allows and wave is not finished spawning
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];//initialise enemy types to spawn
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];//initialize spawn points
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);//spawn enemy at spawn points
            currentWave.numberOfEnemies--;//reduce number of enemies left in wave by 1
            nextSpawnTime = Time.time + currentWave.spawnInterval;//increase next spawn time by spawn time interval
            if (currentWave.numberOfEnemies == 0)//if no enemies left to spawn in wave
            {
                canSpawn = false;//disable spawning
            }
        }

    }

    private void SpawnNextWave()//code to move to next wave
    {
        currentWaveNumber++;//move to next wave
        canSpawn = true;//allow spawning again
    }
}
