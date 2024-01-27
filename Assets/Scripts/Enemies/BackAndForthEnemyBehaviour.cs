using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForthEnemy : MonoBehaviour
{
    private GameObject Player;
    private LayerMask playerLayers;
    private int impactDamage = 10;

    private float nextDamageTime;
    private float damageCooldownTime = 0.5f;

    private void Start()
    {
        playerLayers = LayerMask.NameToLayer("Player");
        Player = GameObject.FindGameObjectWithTag("Player");//update total enemy counter
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && nextDamageTime < Time.time)
        {
            Player.GetComponent<PlayerLife>().TakeDamage(impactDamage);
            Debug.Log("HitPlayer");
            nextDamageTime = Time.time + damageCooldownTime;//increase next damage time by damage cool down
        }
    }
}
