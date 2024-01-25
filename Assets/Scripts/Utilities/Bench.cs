using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour
{
    PlayerLife playerLife;
    AudioSource maxHealthSoundEffect;
    public HealthBarScript healthbar;
    public int lampIndex;
    public int lampSceneIndex;
    public static int lastLampIndex;
    public static int lastLampSceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        playerLife = GameObject.Find("Player").GetComponent<PlayerLife>();
        maxHealthSoundEffect = gameObject.GetComponent<AudioSource>();
        healthbar = GameObject.Find("HealthBar").GetComponent<HealthBarScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerLife.CurrentHealth = playerLife.MaxHealth;//reset health to max
            maxHealthSoundEffect.Play();//play sound effect
            healthbar.SetHealth(playerLife.CurrentHealth);//set health in health bar
            //check if button pressed to set this as new spawn point
            lastLampIndex = lampIndex;
            lastLampSceneIndex = lampSceneIndex;//set as new spawn point (scene and lamp indices)
        }
    }
}
