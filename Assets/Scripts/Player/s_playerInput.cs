using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_playerInput : MonoBehaviour
{
    /// <summary>A reference to a s_weaponwheel class, which handles opening events etc</summary>
    s_weaponWheel m_weaponWheel;
    /// <summary>The left hand, which holds the left weapon</summary>
    s_hand m_leftHand;
    /// <summary>The right hand, which holds the right weapon</summary>
    s_hand m_rightHand;
    GameObject m_player;


    /// <summary>The key to press to fire the left weapon</summary>
    private KeyCode m_leftFireKey = KeyCode.Mouse0;
    /// <summary>The key to press to fire the right weapon</summary>
    private KeyCode m_rightFireKey = KeyCode.Mouse1;
    /// <summary>The key to press to open the weapon wheel</summary>
    private KeyCode m_weaponWheelOpenKey = KeyCode.Mouse2;

    private void Start()
    {
        m_player = gameObject;
        m_weaponWheel = new s_weaponWheel();
        s_hand[] hands = m_player.GetComponentsInChildren<s_hand>();
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
    }

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
}
