using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_hand : MonoBehaviour
{
    [SerializeField] float m_chargeSpeed;
    [SerializeField] s_weapon m_weapon;
    float m_charge = 1.0f;

    public void PullTrigger()
    {
        if (m_weapon.m_chargeCost <= m_charge)                                        //Check if there is enough charge to fire the weapon. If there is...
        {
            m_weapon.Press();                                                         //...Call the weapon that it's triggers been pulled
            m_charge -= m_weapon.m_chargeCost;                                         //Reduce charge according to the weapon's cost
        }
    }

    public void ReleaseTrigger()
    {
        m_weapon.Release();
        
    }

    public void Regen(float elapsedTime)
    {
        if (m_charge < 1.0f)
        {
            m_charge = Mathf.Min(m_charge + (m_chargeSpeed * elapsedTime), 1.0f);    //Multiply that by the charge speed and add it to the charge
        }
    }
}
