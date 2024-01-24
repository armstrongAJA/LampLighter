using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline_animate : MonoBehaviour
{
    //Declare variables
    private Animator anim;
    private float ResetTime = 2f;
    private void Start()
    {
        anim = GetComponent<Animator>();//cache animator for efficiency
    }
    
    private void OnTriggerEnter2D(Collider2D collision)//check for trigger collision
    {
            if (collision.gameObject.CompareTag("Player")) //If There is a trigger collision with the player
            {
                anim.SetBool("TrampolineActive", true);//set trampoline active in animator
                Invoke("ResetTrampoline", ResetTime);//wait for reset time and call ResetTime method
            }
            else
            {
                anim.SetBool("TrampolineActive", false);//if no trigger collision with player, reset trampoline activity in animator
            }
    }

    private void ResetTrampoline()//function to reset the trampoline
    {
        anim.SetBool("TrampolineActive", false);//Reset trampoline activity to false in animator
    }
}
