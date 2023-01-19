using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weaponManager : MonoBehaviour
{
    /// <summary>The left hand, which holds the left weapon</summary>
    [SerializeField] s_hand m_leftHand;
    /// <summary>The right hand, which holds the right weapon</summary>
    [SerializeField] s_hand m_rightHand;
    GameObject m_leftWeapon;
    GameObject m_rightWeapon;
    private bool m_weaponWheel;

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


        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            //open weapon wheel
            Debug.Log("Wheapon Wheel");

            if (m_weaponWheel == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                m_weaponWheel = false;
                Time.timeScale = 1f;

            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                m_weaponWheel = true;
                Time.timeScale = 0.5f;
            }
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