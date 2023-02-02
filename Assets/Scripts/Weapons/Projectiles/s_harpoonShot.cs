using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_harpoonShot : s_stickyProjectile
{
    /// <summary>The gun that fired the harpoon</summary>
    public s_harpoon m_owner;

    /// <summary>Once something has entered the trigger i.e. the harpoon has hit an object</summary>
    /// <param name="other">The other thing that entered the trigger i.e. whatever the object attatched itself to</param>
	protected void OnTriggerEnter(Collider other)
    {
        if (!m_stuck)                               //If we havent yet attatched ourselves to something...
        {
            if (other.gameObject.tag != "Player")   //And whatever we've hit isnt a player
            {
                m_stuck = Stick(other.gameObject);  //Stick to whatever we hit
            }
        }
    }

    /// <summary>Check to see if other is either the ground, or contains a rigidbody. If they do, stick to them and return true. Otherwise, return false.</summary>
    /// <param name="other">The object to try to stick to</param>
    /// <returns>Whether or not the harpoon is stuck</returns>
    override protected bool Stick(GameObject other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())         //if we've hit an object with a rigid body...
        {
            StickToRigidbody(other.GetComponent<Rigidbody>());  //...Stick to it
            m_owner.AttachHarpoon();
            return true;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) //If we've hit the ground...
        {
            StickToGround(other);
            m_owner.AttachHarpoon();
            return true;
        }
        else
        {
            return false;
        }
    }
}
