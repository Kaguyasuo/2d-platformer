using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour

{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoint = 0;

    [SerializeField] private float platformVelocity = 2f;

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypoint].transform.position, transform.position) < .1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, Time.deltaTime * platformVelocity);

    }
}
