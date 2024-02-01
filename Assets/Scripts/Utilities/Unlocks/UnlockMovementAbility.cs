using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockMovementAbility : MonoBehaviour
{
    BoxCollider2D coll;
    public PlayerData playerData;

    [Header("Movements to Unlock")]
    public bool dash = false;
    public bool wallJump = false;
    public bool doubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.Log("BoxTriggered");
            UnlockMovement();
        }
    }
    private void UnlockMovement()
    {
        if (dash)
        {
            playerData.dashActive = true;
        }
        if (wallJump)
        {
            playerData.wallJumpActive = true;
        }
        if (doubleJump)
        {
            playerData.doubleJumpActive = true;
        }
        Destroy(gameObject);
    }
}
