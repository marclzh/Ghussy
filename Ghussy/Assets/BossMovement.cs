using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class BossMovement : StateMachineBehaviour
{
    private Transform player;
    private Rigidbody2D rb;

    // A* Pathfinding
    private float nextWaypointDistance = 0.1f;
    private Seeker seeker;
    private Path path;
    private int currentWaypoint = 0;

    // Enemy Movement Variables
    [SerializeField] private float speed;
    [SerializeField] private float attackRadius;
    private Vector3 dir;

    // Attacking Variables
    private bool isInAttackRange;

    // private since should be following AI's target
    [SerializeField] private Transform target;
    public LayerMask playerLayer;

    // Function repeater
    float lastCall = 0f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        seeker = animator.GetComponent<Seeker>();
        
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isInAttackRange = Physics2D.OverlapCircle(rb.position, attackRadius, playerLayer);
        RepeatPathUpdate(0.5f);

        if (isInAttackRange)
        {
            animator.SetTrigger("Attack");
        }

        // if there is no path
        if (path == null)
        {
            return;
        }

        // check if there are still waypoints, to stop moving
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
       
        // Movement Logic
        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized; // Find the direction to the target.
        Vector2 force = dir * speed * Time.fixedDeltaTime; // add a force in the direction of where we want the enemy to go.

        if (!isInAttackRange)
        {
            rb.AddForce(force);

        }

        // finds the distance between the enemy and the target
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        LookAtPlayer(animator, force);
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }


    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void RepeatPathUpdate(float repeatTime)
    {
        
        if (Time.time > lastCall + repeatTime)
        {
            UpdatePath();
            lastCall = Time.time;
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

    private void LookAtPlayer(Animator animator, Vector2 force)
    {
        if (rb.velocity.x <= 0.001f && force.x < 0f)
        {
          
            animator.GetComponent<Transform>().localScale = new Vector3(-2.5f, 2, 0); 
        }
        else if (rb.velocity.x >= -0.001 && force.x > 0f)
        {

            animator.GetComponent<Transform>().localScale = new Vector3(2.5f, 2, 0);
        }
    }

}
