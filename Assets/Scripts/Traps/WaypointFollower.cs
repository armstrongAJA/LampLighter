using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    //Declare variables
    [SerializeField] private GameObject[] Waypoints;

    private int CurrentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;
    // Update is called once per frame
    private void Update()
    {
        if (Waypoints.Length > 0)
        {
            if (Vector2.Distance(Waypoints[CurrentWaypointIndex].transform.position, transform.position) <
                .1f) //check if touching waypoint
            {
                CurrentWaypointIndex++;//select different waypoint if touching current waypoint
                if (CurrentWaypointIndex >= Waypoints.Length)//check if we have run out of waypoints in array
                {
                    CurrentWaypointIndex = 0;//Reselect first waypoint if moved through full list
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, Waypoints[CurrentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
        //move platform towards selected waypoint (moves tinterval in seconds between frames to make movement frame rate independent)

    }
}
