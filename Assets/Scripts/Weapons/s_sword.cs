using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_sword : s_chargingWeapon
{
    Vector3 m_startingVelocity;
    /// <summary>The force to apply to the player when dashing</summary>
    [SerializeField] float m_dashForce;
    /// <summary>The length the dash will last in seconds</summary>
    [SerializeField] float m_dashDuration;
    /// <summary>The minimum charge required to dash</summary>
    [SerializeField] float m_dashCharge;

    /// <summary>Calls when the dash has finished and resets velocity</summary>
    /// <param name="delay">The delat in seconds, should be set to m_dashDuration</param>
    IEnumerator DashDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        m_rigidBody.velocity = m_startingVelocity;  //Restore the players original velocity
    }

    /// <summary>Checks how long the weapon was charging for. If it exceeded m_dashCharge it will dash, otherwise it will slice</summary>
    override protected void Fire()
    {
        if(m_chargeTime>m_dashCharge)           //If you held long enough to dash...
		{
            if (m_hand.m_charge>=m_chargeCost)  //...and we can afford to dash... 
            {
                Dash();                         //... do so
            }
        }
        else            //If the weapon was not charged long enough...
		{
            Slash();    //Merely slash
		}
    }

    /// <summary>Store the player's starting velocity, and launch them in the direction their facing. Then start the coroutine that brings them out of the dash, and consume charge.</summary>
    private void Dash()
    {
        m_startingVelocity = m_rigidBody.velocity;  //Store the starting velocity
        m_hand.m_charge -= m_chargeCost;            //Consume charge

        m_rigidBody.AddForce(m_camera.TransformDirection(Vector3.forward) * m_dashForce, ForceMode.Impulse);    //Apply the dash force in the forward vector
        StartCoroutine(DashDelay(m_dashDuration));  //Start the coroutine that triggers when the dash delay has ended
    }

    /// <summary>Called when the player just clicks with this weapon. Unimplemented, intended to be a simple melee hit.</summary>
    private void Slash()
    {
        Debug.Log("Slash!");
    }
}
