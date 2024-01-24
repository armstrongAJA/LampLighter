using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogEnemyBehaviour : MonoBehaviour
{
    private GameObject Player;

    private float speed = 3;

    private int impactDamage = 10;

    private float nextDamageTime;

    private float damageCooldownTime = 0.5f;

    private float sightRange = 5;

    private LayerMask playerLayers;
    public float attackDistance = 2f;
    private float attackRange = 3f;
    // Start is called before the first frame update
    void Start()
    {
        playerLayers = LayerMask.NameToLayer("Player");
        Player = GameObject.FindGameObjectWithTag("Player");//update total enemy counter
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] sightedByFrog = Physics2D.OverlapCircleAll(transform.position,
            sightRange); //cast a circle to check if player in view
        foreach (Collider2D sighted in sightedByFrog)//loop over anything found
        {
            if (sighted.CompareTag("Player"))//check if player tag is on any objects returned
            {
                if (Vector2.Distance(transform.position, Player.transform.position) > attackDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position,
                        new Vector2(Player.transform.position.x, transform.position.y), Time.deltaTime * speed);
                    //move enemy towards player if in view and not too close (but only in x to prevent flying enemy if player jumps)
                }
                else
                {
                    Attack();
                }
            }
        }
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
    private void OnDrawGizmosSelected()//draw attack hitbox when gizmos is selected
    {
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    public void Attack()
    {
        Collider2D[] sightedByFrog = Physics2D.OverlapCircleAll(transform.position,
            attackRange); //cast a circle to check if player in view//code to perform attack on player
        foreach (Collider2D sighted in sightedByFrog)//loop over anything found
        {
            if (sighted.CompareTag("Player"))//check if player tag is on any objects returned
            {
                Player.GetComponent<PlayerLife>().TakeDamage(impactDamage);
                Debug.Log("HitPlayer");
                nextDamageTime = Time.time + damageCooldownTime;//increase next damage time by damage cool down

            }
        }
    }
}
