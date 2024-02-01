using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundOffWalls : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D coll;
    public float movementSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(movementSpeed,0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Enemy"))
        {
            Turn();
        }
    }
    void Turn()//code to turn around
    {
        rb.velocity = new Vector2(-rb.velocity.x, 0f);
    }
}
