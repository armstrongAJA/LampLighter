using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBossBehaviour : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private Collider2D coll;
    [SerializeField] private LayerMask jumpableGround;

    private float speed = 5f;

    private int impactDamage = 10;

    private float nextDamageTime;

    private float damageCooldownTime = 0.5f;

    private float sightRange = 15;
    private float nextActionTime;

    private LayerMask playerLayers;
    private int relativePlayerPosition;
    public float actionSampleRate;
    private int currentActionNumber;
    public float jumpSpeed = 20f;
    private bool hasJumped = false;
    public float normalGravity = 1;
    public float fallGravityMult = 3;
    // Start is called before the first frame update
    void Start()
    {
        playerLayers = LayerMask.NameToLayer("Player");
        player = GameObject.FindGameObjectWithTag("Player");//update total enemy counter
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D[] sightedByFrog = Physics2D.OverlapCircleAll(transform.position,
            sightRange); //cast a circle to check if player in view
        foreach (Collider2D sighted in sightedByFrog)//loop over anything found
        {
            if (sighted.CompareTag("Player"))//check if player tag is on any objects returned
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(player.transform.position.x, transform.position.y), Time.deltaTime * speed);
                //move enemy towards player if in view (but only in x to prevent flying enemy if player jumps)
            }
        }
        if (nextActionTime < Time.time )
        {
            GenerateAction();
            if (currentActionNumber == 0 && IsGrounded())
            {
                Jump(jumpSpeed);
            }
        }
        if (rb.velocity.y < 0)
        {
            //Higher gravity if falling
            rb.gravityScale = normalGravity * fallGravityMult;
        }


    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && nextDamageTime < Time.time)
        {
            player.GetComponent<PlayerLife>().TakeDamage(impactDamage);
            Debug.Log("HitPlayer");
            nextDamageTime = Time.time + damageCooldownTime;//increase next damage time by damage cool down
        }

    }
    private void OnDrawGizmosSelected()//draw attack hitbox when gizmos is selected
    {
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void Jump(float jumpSpeed)
    {
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse); //adds jump force to player
        hasJumped = true;
        Debug.Log("Jumped");
    }
    private void GenerateAction()
    {
        currentActionNumber = Random.Range(0, 5);
        Debug.Log(currentActionNumber);
        nextActionTime = Time.time + (1/actionSampleRate);
        hasJumped = false;

    }
    public bool IsGrounded() //Function to check if the player is touching the ground
    {
        return (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .2f,
            jumpableGround)); //casts box .1 below collision box to only extend below collision
    }
}
