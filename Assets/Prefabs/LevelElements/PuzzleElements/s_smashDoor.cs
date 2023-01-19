using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_smashDoor : s_trigger
{
    /// <summary>The lowest velocity the collider needs to reach to "slam" the button</summary>
    [SerializeField] float minVelocity = 0.0f;
    [SerializeField] GameObject m_planks;
    [SerializeField] GameObject m_planksSmashed;
    protected bool m_smashed = false;

    public void Smash()
    {
        m_planks.SetActive(false);
        m_planksSmashed.SetActive(true);
        Trigger();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (CheckCollider(other))
        {
            Smash();
        }
    }

    protected bool CheckCollider(Collider other)
    {
        if (!m_smashed) //Check the door isn't already smashed
        {
            if (other.tag == "Player")  //If the player is the one colliding with it,
            {
                {
                    if (CheckColliderVelocity(other))    //Check the player is moving fast enough in the direction of the door
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    protected bool CheckColliderVelocity(Collider other)
    {
        Vector3 colliderVelocityTowardsButton = GetVelocityTowardsButton(other.attachedRigidbody.velocity);    //Get the players velocity in the direction of the door
        print(colliderVelocityTowardsButton);
        return colliderVelocityTowardsButton.magnitude >= minVelocity;  //Check this against the minVelocity.
    }

    protected Vector3 GetVelocityTowardsButton(Vector3 colliderVelocity)
    {
        Vector3 velocity = colliderVelocity;
        Vector3 direction = transform.forward * -1;  //Get the doors backwards direction
        return Vector3.Project(velocity, direction); //Get the velocity of the player in the direction of the door
    }
}
