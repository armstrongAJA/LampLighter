using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Transform Background;
    // Update is called once per frame
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        FollowPlayer();
        BackgroundFollowCamera();
    }

    private void FollowPlayer()//function to make camera follow player
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    private void BackgroundFollowCamera()//function to make background follow the camera
    {
        Background.position = new Vector3(transform.position.x, transform.position.y, Background.position.z);
    }
}
