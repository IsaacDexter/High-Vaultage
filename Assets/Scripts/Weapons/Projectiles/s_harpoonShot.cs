using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class s_harpoonShot : s_stickyProjectile
{
    /// <summary>The gun that fired the harpoon</summary>
    public s_harpoon m_owner;
    public GameObject m_ropePoint;
    public GameObject m_rope;

    private void FixedUpdate()
	{
        if (!m_stuck)
        {
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            gameObject.transform.rotation = Quaternion.LookRotation(velocity.normalized, Vector3.up);
        }
	}




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

        if (other.gameObject.transform.root.gameObject.tag != "Player" && other.gameObject.transform.root.gameObject.tag != "Projectile" && other.gameObject.transform.root.gameObject.tag != "EnemyBullet")
        {
            if (m_stuck != true)
            {
                Debug.Log("Poon");
                Vector3 colisionPoint = other.contacts[0].point;
                Debug.Log(colisionPoint);
                Debug.Log(other.gameObject.transform.position);
                ParentConstraint constraint = GetComponent<ParentConstraint>();
                constraintSource.sourceTransform = other.gameObject.transform;

                Vector3 tOffset = colisionPoint - other.gameObject.transform.position;
                Vector3 rOffset = gameObject.transform.rotation.eulerAngles - other.gameObject.transform.rotation.eulerAngles;

                Debug.Log(other.gameObject);
                constraintSource.weight = 1;
                constraint.constraintActive = true;
                gameObject.transform.position = colisionPoint;
                //constraint.locked = true;
                constraint.AddSource(constraintSource);
                constraint.SetTranslationOffset(0, tOffset);
                constraint.SetRotationOffset(0, rOffset);
                //constraint.translationOffsets[0] = offset;

                //gameObject.transform.SetParent(collision.gameObject.transform,true);
                m_rigidbody.isKinematic = true;
                //m_rigidbody.
            }
            m_owner.AttachHarpoon();
            return true;
        }
        else
        {
            return false;
        }
    }
}
