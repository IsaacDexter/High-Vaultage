using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_grenadeShot : s_stickyProjectile
{
    /// <summary>Once something has entered the trigger i.e. the grenade has hit an object</summary>
    /// <param name="other">The other thing that entered the trigger i.e. whatever the object attatched itself to</param>
	private void OnTriggerEnter(Collider other)
    {
        if (!m_stuck)                               //If we havent yet attatched ourselves to something...
        {
            if (other.gameObject.tag != "Player")   //And whatever we've hit isnt a player
            {
                m_stuck = Stick(other.gameObject);  //Stick to whatever we hit
            }
        }
    }
}
