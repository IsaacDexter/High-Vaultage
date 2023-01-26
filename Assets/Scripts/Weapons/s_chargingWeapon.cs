using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_chargingWeapon : s_weapon
{
    /// <summary>If the weapon is currently being charged, i.e. the trigger is being held.</summary>
    protected bool m_charging = false;
    /// <summary>The time stored when the player begins charging.</summary>
    protected float m_startTime = 0.0f;
    /// <summary>How long the weapon has been charging. Used to modify the power of certain fire functions.</summary>
    protected float m_chargeTime = 0.0f;
    [Header("Charge Settings")]
    /// <summary>Called when the trigger starts to be held. Stops the hand from regening ammo and starts charging in update.</summary>
    [SerializeField] protected float m_chargeCost = 0.0f;

    override public void Press()
    {
        m_startTime = Time.time;    //Store the time the weapon started charging

        m_charging = true;          //Start the weapon charging
        m_hand.m_regening = false;  //Prevent the hand from regenning ammo while charging
    }
    /// <summary>Called when the trigger ceases to be held, or when the weapon is out of charge and can be charged no further</summary>
    public override void Release()
    {
        if (m_charging) //Check the weapon was charging, to stop release events being triggered multiple times
        {
            Cancel();
            Fire();                                 //Launch the projectile
        }
    }

    override public void Cancel()
    {
        if (m_charging) //Check the weapon was charging, to stop release events being triggered multiple times
        {
            m_charging = false;

            m_chargeTime = Time.time - m_startTime; //Store how long the weapon was charging, used as a modifier in certain fires
            m_hand.m_regening = true;               //Start the hand regening ammo once again
        }
    }


    /// <summary>MUST GO AT BOTTOM Builds up charge in the weapon so long as there is enough charge or Ammo to accomodate it. Overwrite for weapons which do things while charging, e.g. time slow</summary>
    virtual protected void Charge()
    {
        if (m_hand.m_charge - (m_chargeCost * Time.deltaTime) > m_cost) //Check if there would be enough charge to fire the weapon next frame. If there is...
        {
            m_hand.m_charge = Mathf.Max(m_hand.m_charge - (m_chargeCost * Time.deltaTime), 0.0f); //Reduce charge according to the weapon's cost
        }
        else
        {
            Release();  //If we're out of charge, stop charging the weapon and fire it.
        }
    }
    /// <summary>Charges the weapon if necessary</summary>
    override protected void Update()
    {
        if (m_charging) //If were charging,
        { 
            Charge();   //Charge
        }
    }
}
