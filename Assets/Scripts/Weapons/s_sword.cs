using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_sword : s_chargingWeapon
{
    [SerializeField] float m_minCharge;
    float m_holdTimer;
    float m_dashTime;
    Vector3 startingVelcity;
    [SerializeField] float m_dashForce;
    [SerializeField] float m_dashDuration;
    [SerializeField] private GameObject m_meleeBox;

    /// <summary>Sends the player upward according to cameras upwards vector with force proportional to the time spent charging</summary>
    /// 
    IEnumerator DashDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        m_rigidBody.velocity = startingVelcity;
    }

    override protected void Fire()
    {
        if(m_chargeTime>m_minCharge)
		{
            if (m_hand.m_charge>=m_chargeCost)
            {
                Vector3 startingVelcity = m_rigidBody.velocity;
                m_rigidBody.AddForce(m_camera.TransformDirection(Vector3.forward) * m_dashForce, ForceMode.Impulse);
                StartCoroutine(DashDelay(m_dashDuration));
                m_hand.m_charge -= m_chargeCost;
            }

        }
        else
		{
            Debug.Log("Slash");
		}
    }

	protected override void Charge(float elapsedTime)
	{
        print("charging sword...");
		base.Charge(elapsedTime);
	}
}
