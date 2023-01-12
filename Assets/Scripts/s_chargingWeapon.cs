using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_chargingWeapon : s_weapon
{
    protected bool m_charging = false;
    protected float m_startTime = 0.0f;
    protected float m_chargeTime = 0.0f;
    protected float m_time = 0.0f;
    // Update is called once per frame
    override public void Press()
    {
        m_startTime = Time.time;
        m_time = Time.time;
        m_charging = true;
        m_hand.m_regening = false;
    }

    public override void Release()
    {
        if (m_charging)
        {
            m_chargeTime = Time.time - m_startTime;
            m_charging = false;
            m_hand.m_regening = true;
            Fire();
        }
    }

    virtual protected void Charge(float elapsedTime)
    {
        if (m_hand.m_charge > 0) //Check if there is enough charge to charge the weapon. If there is...
        {
            m_hand.m_charge = Mathf.Max(m_hand.m_charge - (m_chargeCost * elapsedTime), 0.0f); //Reduce charge according to the weapon's cost
        }
        else
        {
            Release();
        }
    }

    override protected void Update()
    {
        float elapsedTime = Time.time - m_time;
        if (m_charging)
        {
            Charge(elapsedTime);
        }
        m_time = Time.time;
    }
}
