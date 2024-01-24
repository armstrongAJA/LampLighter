using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)//check for a collision trigger
    {
        if (collision.gameObject.name == "Player")//check if collision is with the player
        {
            collision.gameObject.transform.SetParent(transform);//Parent player motion to the platform while standing on it
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//check for exiting collision
    {
        if (collision.gameObject.name == "Player")//check if collision exit is with player
        {
            collision.gameObject.transform.SetParent(null);//remove parenting from player to uncouple from platform
        }
    }
}
