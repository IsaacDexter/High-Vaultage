using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_chargingWeapon : s_weapon
{
    /// <summary>If the weapon is currently being charged, i.e. the trigger is being held</summary>
    protected bool m_charging = false;
    protected float m_startTime = 0.0f;
    /// <summary>How long the weapon has been charging. Used to modify the power of certain fire functions.</summary>
    protected float m_chargeTime = 0.0f;
    /// <summary>Called when the trigger starts to be held. Stops the hand from regening ammo and starts charging in update</summary>


    override public void Press()
    {
        m_startTime = Time.time;

        m_charging = true;          //Start the weapon charging
        m_hand.m_regening = false;  //Prevent the hand from regenning ammo while charging
    }
    /// <summary>Called when the trigger ceases to be held, or when the weapon is out of charge and can be charged no further</summary>
    public override void Release()
    {
        if (m_charging) //Check the weapon was charging, to stop release events being triggered multiple times
        {
            m_charging = false;

            m_chargeTime = Time.time - m_startTime; //Store how long the weapon was charging, used as a modifier in certain fires
            m_hand.m_regening = true;               //Start the hand regening ammo once again
            Fire();                                 //Launch the projectile
        }
    }


    /// <summary>Builds up charge in the weapon so long as there is enough charge or Ammo to accomodate it. Overwrite for weapons which do things while charging, e.g. time slow</summary>
    /// <param name="elapsedTime">The time elapsed since the last frame.</param>
    virtual protected void Charge(float elapsedTime)
    {
        if (m_hand.m_charge > 0) //Check if there is enough charge to charge the weapon. If there is...
        {
            m_hand.m_charge = Mathf.Max(m_hand.m_charge - (m_chargeCost * elapsedTime), 0.0f); //Reduce charge according to the weapon's cost
        }
        else
        {
            Release();  //If we're out of charge, stop charging the weapon and fire it.
        }
    }
    /// <summary>Calculates elapsed time and charges according to it if we're charging the weapon</summary>
    override protected void Update()
    {
        if (m_charging)
        { 
            Charge(Time.deltaTime);    //Charge according to that time
        }
    }


}
