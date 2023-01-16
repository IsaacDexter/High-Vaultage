using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_swappingPlatform : s_movingPlatform
{
    /// <summary>If the platform has made it to its final destination, in which case it doesn't need to move.</summary>
    protected bool m_arrived = false;
    protected override s_waypoint GetNextWaypoint()
    {
        if (m_active)   //If we're active, proceed through the array forward
        {
            m_waypointIndex++;  //Increment the waypoint index to get the next waypoint
            if (m_waypointIndex >= m_waypoints.Count)  //If we've exceeded the waypoint array...
            {
                m_waypointIndex = m_waypoints.Count - 1;  //Return to the final index
                m_arrived = true;   //Indicate we've arrived
            }
            return m_waypoints[m_waypointIndex];    //Return the value of the array
        }
        else    //Otherwise, return backwards along the path
        {
            m_waypointIndex--;  //Decrement the waypoint index to get the previous waypoint
            if (m_waypointIndex < 0)  //If we've exceeded the waypoint array...
            {
                m_waypointIndex = 0;  //Return to the first index
                m_arrived = true;   //Indicate we've arrived
            }
            return m_waypoints[m_waypointIndex];    //Return the value of the array
        }
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (!m_arrived) //Make sure not to waste time by moving if the platform is at its destination
        {
            MoveToDestination(Time.deltaTime);
        }
    }

    override public void Activate()
    {
        base.Activate();
        m_arrived = false;  //Set that we haven't arrived at our final destination
    }

    override public void Deactivate()
    {
        base.Deactivate();
        m_arrived = false;  //Set that we haven't arrived at our final destination
    }
}
