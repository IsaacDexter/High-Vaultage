using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_shotgun : s_weapon
{
    [SerializeField] private float m_force;

    override protected void Fire()
    {
        Vector3 direction = transform.parent.forward; //Get the players's forwards direction
        m_rigidBody.AddForce(direction * -1.0f * m_force, ForceMode.Impulse);   //Use recoil to move the rigidbody back
        Debug.DrawRay(transform.position, direction, Color.green, 14);
    }
}