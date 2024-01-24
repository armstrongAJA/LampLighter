using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour
{
    PlayerLife playerLife;
    AudioSource maxHealthSoundEffect;
    public HealthBarScript healthbar;
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
            playerLife.CurrentHealth = playerLife.MaxHealth;
            maxHealthSoundEffect.Play();
            healthbar.SetHealth(playerLife.CurrentHealth);
        }
    }
}
