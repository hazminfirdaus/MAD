using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    // create an array to handle more complicated movements in the future instead of defining definite
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    // initialise speed as 2f
    [SerializeField] private float speed = 2f;

    private void Update()
    {
        // check the distance between the moving platform and currently active waypoint
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        // use MoveTowards to calculate the next position
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
