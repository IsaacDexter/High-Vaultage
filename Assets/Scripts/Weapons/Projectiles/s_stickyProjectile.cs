using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_stickyProjectile : MonoBehaviour
{
    /// <summary>The grenade's physics body.</summary>
    protected Rigidbody m_rigidbody;
    /// <summary>Is the grenade stuck to the something?</summary>
    protected bool m_stuck;

    protected void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();    //Get a reference to the rigidbody component
    }

    /// <summary>Check to see if other is either the ground, or contains a rigidbody. If they do, stick to them and return true. Otherwise, return false.</summary>
    /// <param name="other">The object to try to stick to</param>
    /// <returns>Whether or not the grenade is stuck</returns>
	virtual protected bool Stick(GameObject other)
    {
        if (other.gameObject.transform.root.gameObject.tag != "Player"&& other.gameObject.transform.root.gameObject.tag != "Projectile" && other.gameObject.transform.root.gameObject.tag != "EnemyBullet")
        {
            if (m_stuck != true)
            {
                Debug.Log("Poon");
                Vector3 colisionPoint = other.contacts[0].point;
                Debug.Log(colisionPoint);
                Debug.Log(other.gameObject.transform.position);
                ParentConstraint constraint = GetComponent<ParentConstraint>();
                constraintSource.sourceTransform = other.gameObject.transform;

                Vector3 offset = colisionPoint-other.gameObject.transform.position;

                Debug.Log(other.gameObject);
                constraintSource.weight = 1;
                constraint.constraintActive = true;
                gameObject.transform.position = colisionPoint;
                //constraint.locked = true;
                constraint.AddSource(constraintSource);
                constraint.SetTranslationOffset(0,offset);
                //constraint.translationOffsets[0] = offset;

                //gameObject.transform.SetParent(collision.gameObject.transform,true);
                m_rigidbody.isKinematic = true;
                //m_rigidbody.
                return true;
            }
            return true;
        }
        else
        {
            return false;
		}
    }

    /// <summary>Stick to the ground by becoming kinematic</summary>
    /// <param name="other">The ground to stick to. Unused.</param>
    protected void StickToGround(GameObject other)
    {
        m_rigidbody.isKinematic = true;
    }

    /// <summary>Stick to a rigidbody by adding a fixed joint between it and the grenade</summary>
    /// <param name="rigidbody">The rigidbody to stick to</param>
    protected void StickToRigidbody(Rigidbody rigidbody)
    {
        gameObject.AddComponent<FixedJoint>();  //Add a fixed joint to the grenade
        gameObject.GetComponent<FixedJoint>().connectedBody = rigidbody;   //And connect it to the other objects rigidbody
        //m_rigidbody.isKinematic = true; //make this kinematic i.e. fixed
    }
}
