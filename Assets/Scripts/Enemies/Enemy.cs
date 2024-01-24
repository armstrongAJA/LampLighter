using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DictionaryEntry = System.Collections.DictionaryEntry;

public class Enemy : MonoBehaviour
{
    public int MaxHealth = 100;
    private int CurrentHealth;
    private SpriteRenderer sprite;
    private float spriteFlashTime = 0.1f;
    private Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;//initialize enemy health when instantiated
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)//function to take damage
    {
        CurrentHealth -= damage;//reduce health by damage amount
        if (CurrentHealth <= 0)//check if health is reduced to zero
        {
            Die();//implement code to kill off enemy
        }
        StartCoroutine(RenderSprite());//turn sprite off and on after delay (to indicate sprite was damaged visually)
    }

    void Die()//code to kill off enemy
    {
        //die animation
        Debug.Log("Enemy Died");
        GetComponent<Collider2D>().enabled = false;//disable the collider
        enabled = false; //disable this script
        Destroy(gameObject);//destroy the enemy object (dont need previous 2 lines, but could be useful
                            //if implementing on-screen corpses in the future
        
    }

    private IEnumerator RenderSprite()//flash sprite on and off 3 times based on flash time
    {
        sprite.enabled = false;
        yield return new WaitForSeconds(spriteFlashTime);
        sprite.enabled = true;
        yield return new WaitForSeconds(spriteFlashTime);
        sprite.enabled = false;
        yield return new WaitForSeconds(spriteFlashTime);
        sprite.enabled = true;
        yield return new WaitForSeconds(spriteFlashTime);
        sprite.enabled = false;
        yield return new WaitForSeconds(spriteFlashTime);
        sprite.enabled = true;
    }
    
}
