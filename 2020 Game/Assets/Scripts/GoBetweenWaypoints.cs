using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Add to an object to move it through all of the waypoints in its waypoints[] array. It goes back to the first after passing the last.
public class GoBetweenWaypoints : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    int m_CurrentWaypointIndex;

    //Set start destination to first waypoint in array
    void Start ()
    {
        navMeshAgent.SetDestination (waypoints[0].position);
    }

    //If it has reached the current waypoint set destination to the next in the array
    void Update ()
    {
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination (waypoints[m_CurrentWaypointIndex].position);
        }
    }
}
