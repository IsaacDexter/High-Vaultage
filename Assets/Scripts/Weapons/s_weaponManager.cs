using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weaponManager : MonoBehaviour
{
    /// <summary>The left hand, which holds the left weapon</summary>
    [SerializeField] s_hand m_leftHand;
    /// <summary>The right hand, which holds the right weapon</summary>
    [SerializeField] s_hand m_rightHand;

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
    }
}