using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDoorScript : MonoBehaviour
{
    private Animator anim;
    private WaveSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spawner = GetComponentInParent<WaveSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner.battleTriggered)
        {
            anim.SetBool("BattleTriggered", true);
        }
        if (spawner.battleOver)
        {
            anim.SetBool("DoorOpen", true);
        }
        if (!spawner.battleOver)
        {
            anim.SetBool("DoorOpen", false);
        }
    }
}
