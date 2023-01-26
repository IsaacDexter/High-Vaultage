using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_movingPlatform : s_triggerable
{
    /// <summary>The waypoints for the platform to move to, in order of the array given</summary>
    [SerializeField] protected List<s_waypoint> m_waypoints;
    /// <summary>The speed of the moving platform</summary>
    [SerializeField] protected float m_speed = 1.0f;
    /// <summary>The position of the current waypoint</summary>
    protected Vector3 m_destination;
    /// <summary>The index of the current waypoint</summary>
    protected int m_waypointIndex = 0;


    override protected void Start()
    {
        base.Start();
        //Set the initial waypoint to be the first destination, which it should reach instantly.
        m_destination = m_waypoints[0].transform.position;
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (m_active)
        {
            MoveToDestination(Time.deltaTime);
        }
    }
    /// <summary>Finds the direction to the destination, and updates the destination if neccessary. Then moves in that direction</summary>
    virtual protected void MoveToDestination(float elapsedTime)
    {
        //Get the current position
        Vector3 position = transform.position;

        //Find the direction to move in
        Vector3 direction = GetDirection(position);

        //Move in that direction by speed
        transform.position += direction * m_speed * elapsedTime;
    }

    /// <summary>Finds the direction between the positon and the destination and returns it. If we've reached the destination, find the next and the direction to it</summary>
    /// <param name="position">the current position of the object</param>
    /// <returns>The direction to move in</returns>
    protected Vector3 GetDirection(Vector3 position)
    {
        //Find the distance and direction between the position and destination
        Vector3 direction = m_destination - position;
        //Check to see if we've arrived
        if(CheckIfArrived(direction))   //If we have...
        {
            //...Get the next destination
            m_destination = GetNextDestination();
            //Find the distance and direction between the position and new destination
            direction = m_destination - position;
        }
        direction.Normalize();
        return direction;
    }

    /// <param name="distance">destination - position</param>
    /// <returns>If the distance between the two is less than the speed. Updates position to be that position.</returns>
    protected bool CheckIfArrived(Vector3 distance)
    {
        if (distance.magnitude <= m_speed)
        {
            transform.position = m_destination; //Set the position to be the destination (should be imperceptible if we're this close.)
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <returns>Increments m_waypointIndex, or resets it. Returns that waypoint</returns>
    virtual protected s_waypoint GetNextWaypoint()
    {
        m_waypointIndex++;  //Increment the waypoint index to get the next waypoint
        if (m_waypointIndex >= m_waypoints.Count)  //If we've exceeded the waypoint array...
        {
            m_waypointIndex = 0;    //...return to the start of it
        }
        return m_waypoints[m_waypointIndex];
    }

    /// <returns>Increments m_waypointIndex, and returns that waypoints direction</returns>
    protected Vector3 GetNextDestination()
    {
        return GetNextWaypoint().transform.position;
    }
}
