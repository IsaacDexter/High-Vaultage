using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_chargingWeapon : s_weapon
{
    private bool m_charging = false;
    private float m_startTime = 0.0f;
    private float m_chargeTime = 0.0f;
    // Update is called once per frame
    override public void Press()
    {
        m_startTime = Time.time;
        m_charging = true;
    }

    public override void Release()
    {
        m_chargeTime = Time.time - m_startTime;
        m_charging = false;
        Fire();
    }

    virtual protected void Charge()
    {

    }

    override protected void Update()
    {
        if (m_charging)
        {
            Charge();
        }
    }
}
