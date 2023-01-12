using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_fist : s_chargingWeapon
{
    [SerializeField] private float m_force; //The force to launch the player upwards by, multiplied by the time spent charging.
    /// <summary>Sends the player upward according to cameras upwards vector with force proportional to the time spent charging</summary>
    override protected void Fire()
    {
        Vector3 direction = transform.parent.up; //Get the player's cameras upwards direction
        m_rigidBody.AddForce(direction * m_force * m_chargeTime, ForceMode.Impulse);   //Use recoil to move the rigidbody back
    }
}
