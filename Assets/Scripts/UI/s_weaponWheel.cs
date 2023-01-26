using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weaponWheel : s_menu
{
    [HideInInspector] public s_hand m_leftHand { get; private set; }
    [HideInInspector] public s_hand m_rightHand { get; private set; }

    override protected void Start()
    {
        base.Start();
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
    }
}
