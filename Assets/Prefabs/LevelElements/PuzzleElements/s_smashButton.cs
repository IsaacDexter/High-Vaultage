using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_smashButton : s_button
{
    /// <summary>The lowest velocity the collider needs to reach to "slam" the button</summary>
    [SerializeField] float minVelocity = 0.0f;
    protected override bool CheckCollider(Collider other)
    {
        if (base.CheckCollider(other))
        {
            if (CheckColliderVelocity(other))    //Check the player is moving fast enough in the direction of the button
            {
                return true;
            }
        } 
        return false;
    }

    protected bool CheckColliderVelocity(Collider other)
    {
        Vector3 colliderVelocityTowardsButton = GetVelocityTowardsButton(other.attachedRigidbody.velocity);    //Get the players velocity in the direction of the button
        return colliderVelocityTowardsButton.magnitude >= minVelocity;  //Check this against the minVelocity.
    }

    protected Vector3 GetVelocityTowardsButton(Vector3 colliderVelocity)
    {
        Vector3 velocity = colliderVelocity;
        Vector3 direction = transform.up * -1;  //Get the buttons downwards direction
        return Vector3.Project(velocity, direction); //Get the velocity of the player in the direction of the button
    }

    override public void Detrigger()
    {
        return;
    }
}
