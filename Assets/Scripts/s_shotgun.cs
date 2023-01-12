using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_shotgun : s_weapon
{
    /// <summary>The force for the shotgun to knock the player back by</summary>
    [SerializeField] private float m_force;

    /// <summary>Gets the cameras forward vector and launch the player in the opposite direction</summary>
    override protected void Fire()
    {
        Vector3 direction = transform.parent.forward; //Get the players's forwards direction
        m_rigidBody.AddForce(direction * -1.0f * m_force, ForceMode.Impulse);   //Use recoil to move the rigidbody back
    }
}