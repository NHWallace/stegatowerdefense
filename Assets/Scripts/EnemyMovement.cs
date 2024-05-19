using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();

        target = Waypoints.points[0]; // set initial waypoint
    }
    private void Update()
    {
        // Move towards current waypoint
        Vector3 movementDirection = target.position - transform.position;
        transform.Translate(movementDirection.normalized * enemy.speed * Time.deltaTime);

        // If reached current target waypoint, get next waypoint
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        // If end of path is reached, destroy enemy
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            EndReached();
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    void EndReached()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
