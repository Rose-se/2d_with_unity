using System.Collections;
using UnityEngine;

public class MovingBridge : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 5f;

    private void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned to the MovingBridge script.");
        }
    }

    private void Update()
    {
        if (waypoints.Length == 0)
        {
            return; // No waypoints to move towards.
        }

        MoveTowardsWaypoint();
    }

    private void MoveTowardsWaypoint()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].position, transform.position) < 0.1f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, Time.deltaTime * speed);
    }
}