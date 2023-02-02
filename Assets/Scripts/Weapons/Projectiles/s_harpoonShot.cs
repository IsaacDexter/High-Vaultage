using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class s_harpoonShot : s_stickyProjectile
{
    /// <summary>The gun that fired the harpoon</summary>
    public s_harpoon m_owner;

	/// <summary>Once something has entered the trigger i.e. the harpoon has hit an object</summary>
	/// <param name="other">The other thing that entered the trigger i.e. whatever the object attatched itself to</param>
	private void OnCollisionEnter(Collision other)
    {
        if (!m_stuck)                               //If we havent yet attatched ourselves to something...
        {
            if (other.gameObject.tag != "Player")   //And whatever we've hit isnt a player
            {
                m_stuck = Stick(other);  //Stick to whatever we hit
            }
        }
    }

    /// <summary>Check to see if other is either the ground, or contains a rigidbody. If they do, stick to them and return true. Otherwise, return false.</summary>
    /// <param name="other">The object to try to stick to</param>
    /// <returns>Whether or not the harpoon is stuck</returns>
    override protected bool Stick(Collision other)
    {

        if (other.gameObject.transform.root.gameObject.tag != "Player")
        {
            Debug.Log("Poon");
            Vector3 colisionPoint = other.contacts[0].point;

            ParentConstraint constraint = GetComponent<ParentConstraint>();
            constraintSource.sourceTransform = other.gameObject.transform;
            //constraintSource.weight = 100;

            constraint.AddSource(constraintSource);

            //gameObject.transform.SetParent(collision.gameObject.transform,true);
            gameObject.transform.position = colisionPoint;
            m_rigidbody.isKinematic = true;

            m_owner.AttachHarpoon();
            return true;
        }
        else
        {
            return false;
        }
    }
}
