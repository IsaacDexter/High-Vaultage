using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weaponManager : MonoBehaviour
{
    /// <summary>The left hand, which holds the left weapon</summary>
    [SerializeField] s_hand m_leftHand;
    /// <summary>The right hand, which holds the right weapon</summary>
    [SerializeField] s_hand m_rightHand;
    /// <summary>A reference to a s_weaponwheel class, which handles opening events etc</summary>
    private s_weaponWheel m_weaponWheel = new s_weaponWheel();

    /// <summary>The key to press to fire the left weapon</summary>
    private KeyCode m_leftFireKey = KeyCode.Mouse0;
    /// <summary>The key to press to fire the right weapon</summary>
    private KeyCode m_rightFireKey = KeyCode.Mouse1;
    /// <summary>The key to press to open the weapon wheel</summary>
    private KeyCode m_weaponWheelOpenKey = KeyCode.Mouse2;




    /// <summary>Handles the users input events and prompts the hands to regenerate charge</summary>
    void Update()
    {
        if (Input.GetKeyDown(m_leftFireKey))
        {
            m_leftHand.PullTrigger();
        }
        if (Input.GetKeyUp(m_leftFireKey))
        {
            m_leftHand.ReleaseTrigger();
        }
        if (Input.GetKeyDown(m_rightFireKey))
        {
            m_rightHand.PullTrigger();
        }
        if (Input.GetKeyUp(m_rightFireKey))
        {
            m_rightHand.ReleaseTrigger();
        }

        if (Input.GetKeyDown(m_weaponWheelOpenKey))
        {
            m_weaponWheel.ToggleWeaponWheel();
        }
    }
    public void SwitchWeapon(GameObject weapon, float arm)
    {
        if (arm == 1)
        {
            m_leftHand.Equip(weapon);
        }
        if (arm == 2)
        {
            m_rightHand.Equip(weapon);
        }
    }
}