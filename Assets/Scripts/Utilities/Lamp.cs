using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
<<<<<<< HEAD
    public PlayerData playerData;
    AudioSource maxHealthSoundEffect;
    public HealthBarScript healthbar;
    public LampData lamp;
    public Transform lampTransform;

    // Start is called before the first frame update
    void Start()
    {
=======
    PlayerLife playerLife;
    AudioSource maxHealthSoundEffect;
    public HealthBarScript healthbar;
    public LampData lamp;
    public static int lastLampIndex;
    public static int lastLampSceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        playerLife = GameObject.Find("Player").GetComponent<PlayerLife>();
>>>>>>> 3db50ad510a1c15cb720fb44c6abfea912c39737
        maxHealthSoundEffect = gameObject.GetComponent<AudioSource>();
        healthbar = GameObject.Find("HealthBar").GetComponent<HealthBarScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
<<<<<<< HEAD
            playerData.CurrentHealth = playerData.MaxHealth;//reset health to max
            maxHealthSoundEffect.Play();//play sound effect
            healthbar.SetHealth(playerData.CurrentHealth);//set health in health bar
            //check if button pressed to set this as new spawn point
            playerData.lastLamp.lampIndex = lamp.lampIndex;
            playerData.lastLamp.lampSceneIndex = lamp.lampSceneIndex;
            Debug.Log("new last lamp scene index:" + lamp.lampSceneIndex);
=======
            playerLife.CurrentHealth = playerLife.MaxHealth;//reset health to max
            maxHealthSoundEffect.Play();//play sound effect
            healthbar.SetHealth(playerLife.CurrentHealth);//set health in health bar
            //check if button pressed to set this as new spawn point
            lastLampIndex = lamp.lampIndex;
            lastLampSceneIndex = lamp.lampSceneIndex;//set lamp as new spawn point (scene and lamp indices)
>>>>>>> 3db50ad510a1c15cb720fb44c6abfea912c39737
        }
    }
}
