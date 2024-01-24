using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObjectScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] private float flipRotationTime = 0.5f;
    private Coroutine turnCoroutine;
    private PlayerMovementOriginal player;
    private bool isFacingRight;

    private void Awake()
    {
        player = playerTransform.gameObject.GetComponent<PlayerMovementOriginal>();//get movement script from player object
        isFacingRight = player.isFacingRight;//get facing right bool from player movement script
    }
    private void Update()
    {
        transform.position = playerTransform.position;//update object position based on players position

    }
    public void CallTurn()
    {
        LeanTween.rotateY(gameObject, DetermineEndRotation(), flipRotationTime).setEaseInOutSine();//turn the object smoothly
                                                                                                   //using tweening to ensure smooth interpolation
    }
    private float DetermineEndRotation()
    {
        isFacingRight = !isFacingRight;//invert the isFacingRight bool
        if (isFacingRight)//perform rotation to 0 degrees if facing right
        {
            return 0f;
        }
        else//perform rotation to 180 degrees if facing left
        {
            return 180f;
        }
    }
   
    
}
