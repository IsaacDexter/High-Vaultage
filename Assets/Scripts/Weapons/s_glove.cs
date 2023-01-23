using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_glove : s_chargingWeapon
{
    [Header("Uppercut Settings")]
    /// <summary>The force to launch the player upwards by, multiplied by the time spent charging.</summary>
    [SerializeField] private float m_uppercutForce;
    /// <summary>The minimum force to be launched upwards by</summary>
    [SerializeField] private float m_minForce;
    /// <summary>The maximum force to be launched forwards by</summary>
    [SerializeField] private float m_maxForce;
    /// <summary>The amount of charge required to uppercut instead of punch</summary>
    [SerializeField] private float m_uppercutCharge;
    [Header("Punch Settings")]
    /// <summary>How much it costs to perfom an punch as opposed to a big uppercut</summary>
    [SerializeField] private float m_punchCost;

    /// <summary>Sends the player upward according to cameras upwards vector with force proportional to the time spent charging</summary>
    override protected void Fire()
    {
        if(m_chargeTime>m_uppercutCharge)   //If we've accumulated enough charge to perform an uppercut...
		{
            if(CheckCost())                 //...And can afford it...
            {
                Uppercut();                 //...Uppercut.
            }
        }
        else                        //If we've only accumulated enough charge to perform a punch...
		{
            if (CheckPunchCost())   //...And can afford it...
            {
                Punch();            //...Punch.
            }
		}
    }

    private void Uppercut()
    {
        Vector3 direction = m_rigidBody.transform.up;   //Get the player's upwards direction
        m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, Mathf.Max(m_rigidBody.velocity.y, 0), m_rigidBody.velocity.z);   //Set the y to be at least 0
        float force = Mathf.Clamp(m_uppercutForce * m_chargeTime, m_minForce, m_maxForce);   //Calculate how much force to send the player up by
        m_rigidBody.AddForce(direction * force, ForceMode.Impulse);   //Apply an upwards force to the player
    }

    private void Punch()
    {
        Debug.Log("Punch!");
    }

    private bool CheckPunchCost()
    {
        if (m_hand.m_charge >= m_punchCost)    //If we can afford to punch...
        {
            m_hand.m_charge -= m_punchCost;    //...Pay the cost.
            return true;
        }
        else
        {
            return false;
        }
    }
}
