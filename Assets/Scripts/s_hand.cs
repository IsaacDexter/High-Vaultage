using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_hand : MonoBehaviour
{
    [SerializeField] float m_chargeSpeed;
    [SerializeField] s_weapon m_weapon;
    public bool m_regening = true;
    public float m_charge = 1.0f;

    public void PullTrigger()
    {
        if (m_weapon != null)
        {
            m_weapon.Press();                                                         //...Call the weapon that it's triggers been pulled
        }
    }

    public void ReleaseTrigger()
    {
        if (m_weapon != null)
        {
            m_weapon.Release();
        }
    }

    public void Regen(float elapsedTime)
    {
        if (m_regening)
        {
            if (m_charge < 1.0f)
            {
                m_charge = Mathf.Min(m_charge + (m_chargeSpeed * elapsedTime), 1.0f);    //Multiply that by the charge speed and add it to the charge
            }
        }
    }
}
