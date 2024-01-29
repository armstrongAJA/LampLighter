using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lamp : MonoBehaviour
{
    public PlayerData playerData;
    AudioSource maxHealthSoundEffect;
    public HealthBarScript healthbar;
    public LampData lamp;
    private bool triggerStay = false;
    public TMP_Text interactTextPrompt;

    // Start is called before the first frame update
    private void Start()
    {
        maxHealthSoundEffect = gameObject.GetComponent<AudioSource>();
        healthbar = GameObject.Find("HealthBar").GetComponent<HealthBarScript>();
    }
    private void Update()
    {
        if (triggerStay && (playerData.lastLamp.lampIndex != lamp.lampIndex || playerData.lastLamp.lampSceneIndex !=lamp.lampSceneIndex))//if player within collider and lamp not yet lit
        {
            interactTextPrompt.enabled = true;

            if (Input.GetButtonDown("Submit"))//if player activates the lamp
            {
                playerData.CurrentHealth = playerData.MaxHealth;//reset health to max
                maxHealthSoundEffect.Play();//play sound effect
                healthbar.SetHealth(playerData.CurrentHealth);//set health in health bar
                                                              //check if button pressed to set this as new spawn point
                playerData.lastLamp.lampIndex = lamp.lampIndex;
                playerData.lastLamp.lampSceneIndex = lamp.lampSceneIndex;//set lamp as new spawn point (scene and lamp indices)
                lamp.isLampLit = true;//light lamp
            }
        }
        else
        {
            interactTextPrompt.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggerStay = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggerStay = false;
        }
        
    }
}
