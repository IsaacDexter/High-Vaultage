using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weaponManager : MonoBehaviour
{
    /// <summary>The left hand, which holds the left weapon</summary>
    [SerializeField] s_hand m_leftHand;
    /// <summary>The right hand, which holds the right weapon</summary>
    [SerializeField] s_hand m_rightHand;

    /// <summary>If the weapon wheel is open at the moment.</summary>
    private bool m_weaponWheelOpen = false;
    /// <summary>The key to press to open the weapon wheel</summary>
    private KeyCode m_weaponWheelOpenKey = KeyCode.Mouse2;
    /// <summary>The speed time should move at when the weapon wheel is open</summary>
    private float m_weaponWheelTimeDilation = 0.5f;

    /// <summary>Handles the users input events and prompts the hands to regenerate charge</summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_leftHand.PullTrigger();
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_leftHand.ReleaseTrigger();
        }
        if (Input.GetMouseButtonDown(1))
        {
            m_rightHand.PullTrigger();
        }
        if (Input.GetMouseButtonUp(1))
        {
            m_rightHand.ReleaseTrigger();
        }

        if (Input.GetKeyDown(m_weaponWheelOpenKey))
        {
            ToggleWeaponWheel();
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

    /// <summary>If the weapon wheel's open, close it and vice versa.</summary>
    public void ToggleWeaponWheel()
    {
        if (m_weaponWheelOpen)  
        {
            CloseWeaponWheel(); //If open, close
        }
        else
        {
            OpenWeaponWheel();  //If closed, open
        }
    }

    /// <summary>Closes the weapon wheel and locks the cursor</summary>
    private void CloseWeaponWheel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;     //Lock and hide the cursor
        m_weaponWheelOpen = false;  //Update check bool
        Time.timeScale = 1f;        //Speed time up to the normal amount
    }

    /// <summary>Opens the weapons wheel, freeing the cursor and slowing down time</summary>
    private void OpenWeaponWheel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;      //Unlock and show the cursor
        m_weaponWheelOpen = true;   //Update check bool
        Time.timeScale = m_weaponWheelTimeDilation;      //Slow down time to the slow speed
    }
}