using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_fist : s_chargingWeapon
{
    [SerializeField] private float m_force;
    override protected void Fire()
    {
        Vector3 direction = transform.parent.up; //Get the players's upwards direction
        m_rigidBody.AddForce(direction * m_force * m_chargeTime, ForceMode.Impulse);   //Use recoil to move the rigidbody back
        Debug.DrawRay(transform.position, direction, Color.green, 14);
    }
}
