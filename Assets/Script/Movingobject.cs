using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private Transform[] Waypoints;
    [SerializeField] private float speed = 5f;

    private int currentWaypointIndex = 0;

    private void Start()
    {
        if (Waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned to the MovingObject script on GameObject: " + gameObject.name);
        }
    }

    private void Update()
    {
        if (Waypoints.Length == 0)
        {
            return; // No waypoints to move towards.
        }

        MoveTowardsWaypoint();
    }

    private void MoveTowardsWaypoint()
    {
        SkipToNextWaypointIfNeeded();

        if (Waypoints[currentWaypointIndex] != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Waypoints[currentWaypointIndex].position, Time.deltaTime * speed);
        }
        else
        {
            Debug.LogWarning("Waypoint is null. Skipping to the next waypoint.");
            SkipToNextWaypoint();
        }
    }

    private void SkipToNextWaypointIfNeeded()
    {
        if (Vector3.Distance(Waypoints[currentWaypointIndex].position, transform.position) < 0.1f)
        {
            SkipToNextWaypoint();
        }
    }

    private void SkipToNextWaypoint()
    {
        if (Waypoints[currentWaypointIndex] == null)
        {
            Debug.LogWarning("Next waypoint is null.");
        }

        currentWaypointIndex = (currentWaypointIndex + 1) % Waypoints.Length;
    }
}