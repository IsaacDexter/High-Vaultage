using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_hand : MonoBehaviour
{
    /// <summary>The speed that the hand regains charge</summary>
    [SerializeField] float m_chargeSpeed;
    /// <summary>The current weapon the hand is holding. Should be a reference to a child of camera</summary>
    public GameObject m_weapon;
    /// <summary>If the weapon is currently regaining charge (set to false while charging weapons are charging.)</summary>
    public bool m_regening = true;
    /// <summary>The charge (ammo) this fist has, shared between weapons</summary>
    public float m_charge = 1.0f;

    /// <summary>Will call press on the currently held weapon</summary>
    public void PullTrigger()
    {
        if (m_weapon != null)   //Check we are holding a weapon
        {
            //m_regening = false;
            m_weapon.GetComponent<s_weapon>().Press();   //...tell the weapon that it's triggers been pulled
        }
    }

    /// <summary>Will call release on the currently held weapon. Only charging weapons use this.</summary>
    public void ReleaseTrigger()
    {
        if (m_weapon != null)   //Check we are holding a weapon
        {
            //m_regening = false;
            m_weapon.GetComponent<s_weapon>().Release(); //...tell the weapon that it's triggers been pulled
        }
    }

    /// <summary>Regenerates charge or ammo in this hand over time. Will not regenerate if a charge weapon is telling it not to.</summary>
    /// <param name="elapsedTime">The time elapsed since the last frame (Time.time - m_time in most cases)</param>
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

    public void Equip(GameObject weapon)
	{
        Destroy(m_weapon);
        m_weapon = Instantiate(weapon, transform.position, transform.rotation);
        m_weapon.transform.SetParent(transform, true);
        m_weapon.transform.localPosition = new Vector3(0, 0, 0);
        m_weapon.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        m_weapon.GetComponent<s_weapon>().m_hand = this;
    }
	private void Update()
	{
        Regen(Time.deltaTime);
	}
}
