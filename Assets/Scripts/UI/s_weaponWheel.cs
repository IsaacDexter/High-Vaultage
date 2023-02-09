using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weaponWheel : s_menu
{
    [HideInInspector] public s_hand m_leftHand { get; private set; }
    [HideInInspector] public s_hand m_rightHand { get; private set; }
    private s_weaponWheelButton[] m_weaponWheelButtons;

    override protected void Start()
    {
        base.Start();
        m_timeDilation = 0.24f;
        s_hand[] hands = m_player.GetComponentsInChildren<s_hand>();    //Get the players hands
        if (hands[0].name == "m_leftHand")
        {
            m_leftHand = hands[0];
            m_rightHand = hands[1];
        }
        else
        {
            m_leftHand = hands[1];
            m_rightHand = hands[0];
        }
        m_weaponWheelButtons = gameObject.GetComponentsInChildren<s_weaponWheelButton>();
        foreach (s_weaponWheelButton button in m_weaponWheelButtons)
        {
            button.Lock();
        }
    }

    /// <summary>Check each button to see if its weapon is of the same type as the parameter. If it is, call unlock on that button.</summary>
    /// <param name="weapon">The type of weapon to unlock</param>
    public void Unlock(s_weapon weapon)
    {
        foreach (s_weaponWheelButton button in m_weaponWheelButtons)
        {
            if (button.m_weapon.GetComponent<s_weapon>().GetType() == weapon.GetType())  //If this button's weapon is the same as the weapon were trying to unlock...
            {
                button.Unlock();
            }
        }
    }

    /// <summary>Check each button to see if its weapon is of the same type as the parameter. If it is, call lock on that button. Then Checks each of the players hands and dequips that weapon if its of the same type.</summary>
    /// <param name="weapon">The type of weapon to lock</param>
    public void Lock(s_weapon weapon)
    {
        foreach (s_weaponWheelButton button in m_weaponWheelButtons)
        {
            if (button.m_weapon.GetComponent<s_weapon>().GetType() == weapon.GetType())     //If this button's weapon is the same as the weapon were trying to unlock...
            {
                button.Lock();  //Lock it
            }
        }

        if (m_leftHand.m_weapon != null)    //if the left hand is holding a weapon
        {
            if (m_leftHand.m_weapon.GetComponent<s_weapon>().GetType() == weapon.GetType())     //If the left hand is holding a weapon of this type...
            {
                print("Left hand holding " + weapon.GetType());
                m_leftHand.Dequip();    //Dequip it
            }
        }

        if (m_rightHand.m_weapon != null)    //If the right hand is holding a weapon
        {
            if (m_rightHand.m_weapon.GetComponent<s_weapon>().GetType() == weapon.GetType())    //If the left hand is holding a weapon of this type...
            {
                print("Right hand holding " + weapon.GetType());
                m_rightHand.Dequip();    //Dequip it
            }
        }
    }
}
