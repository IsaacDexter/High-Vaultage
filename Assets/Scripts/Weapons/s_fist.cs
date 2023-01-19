using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_fist : s_chargingWeapon
{
    [SerializeField] private float m_force; //The force to launch the player upwards by, multiplied by the time spent charging.
    [SerializeField] private float m_minCharge;
    /// <summary>Sends the player upward according to cameras upwards vector with force proportional to the time spent charging</summary>
    override protected void Fire()
    {
        if(m_chargeTime>m_minCharge)
		{
            Vector3 direction = m_rigidBody.gameObject.transform.up; //Get the player's cameras upwards direction
            float velocityCancel = m_rigidBody.velocity.y;
            if (velocityCancel < 0)
            {
                velocityCancel = 0;
            }
            m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, velocityCancel, m_rigidBody.velocity.z);
            m_rigidBody.AddForce(direction * m_force * (1+m_chargeTime), ForceMode.Impulse);   //Use recoil to move the rigidbody back
            m_hand.m_charge -= m_chargeCost;
        }
        else
		{
            Debug.Log("pung");
		}
    }

	protected override void Charge(float elapsedTime)
	{
        print("charging fist...");
		base.Charge(elapsedTime);
	}
}
