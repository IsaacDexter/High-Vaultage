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
            m_weapon.GetComponent<s_weapon>().Press();   //...tell the weapon that it's triggers been pulled
        }
    }

    /// <summary>Will call release on the currently held weapon. Only charging weapons use this.</summary>
    public void ReleaseTrigger()
    {
        if (m_weapon != null)   //Check we are holding a weapon
        {
            m_weapon.GetComponent<s_weapon>().Release(); //...tell the weapon that it's triggers been pulled
        }
    }

    /// <summary>Regenerates charge or ammo in this hand over time. Will not regenerate if a charge weapon is telling it not to.</summary>
    public void Regen()
    {
        if (m_regening)
        {
            if (m_charge < 1.0f)
            {
                m_charge = Mathf.Min(m_charge + (m_chargeSpeed * Time.deltaTime), 1.0f);    //Multiply that by the charge speed and add it to the charge
            }
        }
    }

    /// <summary>Destroys the current one, and instanciate a new one of the desired weapon type.</summary>
    /// <param name="weapon">The weapon prefab to instanciate</param>
    public void Equip(GameObject weapon)
	{

        Destroy(m_weapon);                                                          //Destroy the currently equipped weapon
        m_weapon = Instantiate(weapon, transform.position, transform.rotation);     //Instanciate a new weapon object

        m_weapon.transform.SetParent(transform, true);                              //Set the weapon to be a child of the hand
        m_weapon.transform.localPosition = new Vector3(0, 0, 0);                    
        m_weapon.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));  //Reset the local position and rotation for that weapon

        m_weapon.GetComponent<s_weapon>().m_hand = this;    //Set the weapon's reference to it's hand

    }
	private void Update()
	{
        Regen();  //Attempt to regenerate ammo
	}
}
