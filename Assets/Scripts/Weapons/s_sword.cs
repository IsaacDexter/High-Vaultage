using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_sword : s_chargingWeapon
{
    [Header("Dash Settings")]
    /// <summary>The force to apply to the player when dashing, proprtional to the amount of time spent charging</summary>
    [SerializeField] float m_dashForce;
    /// <summary>The minimum force to be launched upwards by</summary>
    [SerializeField] private float m_minForce;
    /// <summary>The maximum force to be launched forwards by</summary>
    [SerializeField] private float m_maxForce;
    /// <summary>The length the dash will last in seconds, on top of how long the player charged for</summary>
    [SerializeField] float m_dashDuration;
    /// <summary>The minimum charge required to dash</summary>
    [SerializeField] float m_dashCharge;
    /// <summary>What percentage of the horizontal velocity to retain once the charge has ended</summary>
    [SerializeField] float m_velocityFalloff;
    [Header("Slash Settings")]
    /// <summary>The cost of a small slash as opposed to a big dash.</summary>
    [SerializeField] float m_slashCost;

    /// <summary>Calls when the dash has finished and resets velocity</summary>
    /// <param name="delay">The delat in seconds, should be set to m_dashDuration</param>
    IEnumerator DashDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x * m_velocityFalloff, m_rigidBody.velocity.y, m_rigidBody.velocity.z * m_velocityFalloff);
    }

    /// <summary>Checks how long the weapon was charging for. If it exceeded m_dashCharge it will dash, otherwise it will slice</summary>
    override protected void Fire()
    {
        if(m_chargeTime>m_dashCharge)   //If you held long enough to dash...
		{
            if (CheckCost())            //...and we can afford to dash... 
            {
                Dash();                 //... do so
            }
        }
        else                        //If the weapon was only charged long enough to slash...
		{
            if (CheckSlashCost())   //...And we can afford to slash...
            {
                Slash();            //...Slash.
            }
		}
    }

    /// <summary>Store the player's starting velocity, and launch them in the direction their facing. Then start the coroutine that brings them out of the dash, and consume charge.</summary>
    private void Dash()
    {
        float force = Mathf.Clamp(m_dashForce * m_chargeTime, m_minForce, m_maxForce);  //Calculate the force of the dash proprtional to time spent charging
        m_rigidBody.AddForce(m_camera.forward * force, ForceMode.Impulse);              //Apply the dash force in the forward vector
        StartCoroutine(DashDelay(m_dashDuration));                                      //Start the coroutine that triggers when the dash delay has ended
    }

    /// <summary>Called when the player just clicks with this weapon. Unimplemented, intended to be a simple melee hit.</summary>
    private void Slash()
    {
        Debug.Log("Slash!");
    }

    private bool CheckSlashCost()
    {
        if (m_hand.m_charge >= m_slashCost)    //If we can afford to dash...
        {
            m_hand.m_charge -= m_slashCost;    //...Pay the cost.
            return true;
        }
        else
        {
            return false;
        }
    }
}
