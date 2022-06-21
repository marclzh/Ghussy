using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    // Enemy graphics reference
    public Transform enemyGFX;

    // Target for pathfinding 
    public Transform target;

    // Enemy Pathfinding variables
    public float nextWaypointDistance = 0.1f;
    public bool isChasing = false;
    
    Path path;
    int currentWaypoint = 0;
    private bool reachedEndOfPath;

    Seeker seeker;
    Rigidbody2D rb;
    
    void Start()
    {
        
       
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        // set the path to p only if no errors while calculating the path.
        if (!p.error)
        {
            path = p;
            // reset the position of the current waypoint
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        // if enemy is not chasing anything
        if (!isChasing)
        {
            return;
        }

        // if there is no path
        if (path == null)
        {
            return;
        }
        
        // check if there are still waypoints, to stop moving
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        // to move the enemy
        // 1st find the direction to the target.
        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        // add a force in the direction of where we want the enemy to go.
        Vector2 force = dir * GetComponent<EnemyMovement>().speed * Time.deltaTime;

        if (!GetComponent<EnemyMovement>().isInAttackRange)
        {
            rb.AddForce(force);
        }

        // finds the distance between the enemy and the target
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (enemyGFX != null)
        {
            if (rb.velocity.x <= 0.001f && force.x < 0f)
            {
                enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.velocity.x >= -0.001 && force.x > 0f)
            {
                enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
