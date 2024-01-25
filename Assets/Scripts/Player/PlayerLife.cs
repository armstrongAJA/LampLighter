using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    //TODO: send max health/current health to PlayerData scriptable object

    //Declare variables
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource deathSoundEffect;
    public int MaxHealth = 100;
    public int CurrentHealth;

    private bool dead = false;
    private float nextDamageTime;

    private float damageCooldownTime = 0.5f;
    public HealthBarScript healthbar;
    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();//cache variables to avoid calling inefficient function a lot
        healthbar = GameObject.Find("HealthBar").GetComponent<HealthBarScript>();
        CurrentHealth = MaxHealth;//initialize enemy health when instantiated
        healthbar.SetMaxHealth(MaxHealth);


    }

    private void OnCollisionEnter2D(Collision2D collision)//check for a collision
    {
        if (collision.gameObject.CompareTag("Trap"))//check if collision is with a trap
        {
            Die();//Function for player to die
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)//set trigger to alllow platforms to fall through fall death box
    {
        if (collision.gameObject.CompareTag("Trap"))//if triggered (treating box as trap)
        {
            Die();//Player dies from falling off map
        }
    }

    public void Die()//function for player death
    {
        anim.SetTrigger("Death");//Set death animation running
        rb.bodyType = RigidbodyType2D.Static;//Change player to static bodytype to turn physics off and fix player (so can't die twice)
        deathSoundEffect.Play();//Play death sound effect
        Invoke("RestartLevel", 2f);
    }

    private void RestartLevel()//function to restart the level upon death, 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//reload current scene
    }
    
    public void TakeDamage(int damage)//function to take damage
    {
        if (nextDamageTime < Time.time)
        {
            anim.SetBool("Damage", true); //Set death animation running
            CurrentHealth -= damage; //reduce health by damage amount
            if (CurrentHealth <= 0 && !dead) //check if health is reduced to zero
            {
                Die(); //implement code to kill off player
                dead = true;
            }

            nextDamageTime = Time.time + damageCooldownTime; //increase next damage time by damage cool down
        }
        healthbar.SetHealth(CurrentHealth);
    }
}
