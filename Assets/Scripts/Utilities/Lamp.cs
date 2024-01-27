using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public PlayerData playerData;
    AudioSource maxHealthSoundEffect;
    public HealthBarScript healthbar;
    public LampData lamp;
    public Transform lampTransform;

    // Start is called before the first frame update
    void Start()
    {
        maxHealthSoundEffect = gameObject.GetComponent<AudioSource>();
        healthbar = GameObject.Find("HealthBar").GetComponent<HealthBarScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerData.CurrentHealth = playerData.MaxHealth;//reset health to max
            maxHealthSoundEffect.Play();//play sound effect
            healthbar.SetHealth(playerData.CurrentHealth);//set health in health bar
            //check if button pressed to set this as new spawn point
            playerData.lastLamp.lampIndex = lamp.lampIndex;
            playerData.lastLamp.lampSceneIndex = lamp.lampSceneIndex;
            Debug.Log("new last lamp scene index:" + lamp.lampSceneIndex);
        }
    }
}
