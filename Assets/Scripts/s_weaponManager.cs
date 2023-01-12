using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weaponManager : MonoBehaviour
{
    [SerializeField] s_hand m_leftHand;
    [SerializeField] s_hand m_rightHand;
    private float m_time;

    // Update is called once per frame
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
        Regen();
    }

    private void Regen()
    {
        float elapsedTime = Time.time - m_time;
        m_leftHand.Regen(elapsedTime);
        m_rightHand.Regen(elapsedTime);
        m_time = Time.time;
    }
}