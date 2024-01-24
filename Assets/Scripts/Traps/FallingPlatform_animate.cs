using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatform_animate : MonoBehaviour
{
    //declare various variables
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float ResetTime = 2f;
    [SerializeField] private float fallDelay = 2.0f;
    private Vector3 InitialPosition;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();//cache the relevant properties as scee loads
        InitialPosition = transform.position;//set initial platform position to reseet to after fall
    }
    
    private void OnCollisionEnter2D(Collision2D collision)//when a collision happens
    {
        if (collision.gameObject.CompareTag("Player")) //If There is a collision with the player
        {
            anim.SetBool("PlatformState", false);//turn off platform animation
            StartCoroutine(FallAfterDelay());//call function to drop platform as coroutine to run in parallel
        }
    } 

    IEnumerator FallAfterDelay()//routine to call as coroutine
    {
        yield return new WaitForSeconds(fallDelay);//wait for a time before falling
        rb.isKinematic = false;//turn physics on for platform to fall
        yield return new WaitForSeconds(ResetTime);//wait for Reset time to reset platform at initial position
        transform.position = InitialPosition;//set platform to initial position at scene load
        rb.velocity = new Vector3(0, 0, 0);//set falling platform velocity back to zero
        rb.isKinematic = true;//turn physics off again for platform to fix in place again
        anim.SetBool("PlatformState", true);//Turn on platform animation 
        
    }
}