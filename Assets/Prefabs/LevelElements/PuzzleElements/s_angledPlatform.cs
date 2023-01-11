using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class s_angledPlatform : s_triggerable
{
    [SerializeField] protected GameObject m_hinge;
    [SerializeField] protected GameObject m_platform;
    [SerializeField] protected float m_activeAngle;
    [SerializeField] protected float m_inactiveAngle;
    [SerializeField] protected float m_activeSpeed;
    [SerializeField] protected float m_inactiveSpeed;

    protected float m_currentAngle = 0.0f;
    protected float m_targetAngle;
    protected bool m_swinging = false;
    float m_speed;

    protected void Swing(float angle)
    {
        m_platform.transform.RotateAround(m_hinge.transform.position, m_hinge.transform.forward, angle);
        m_currentAngle += angle;
    }

    protected void SwingTo(float angle)
    {
        Swing(angle - m_currentAngle);
        m_currentAngle = angle;
    }

    /// <returns>if we have reached our target angle</returns>
    protected bool CheckAngle()
    {
        if (m_speed > 0)    //If we have positive speed, check to see if we have exceeded our target angle
        {
            return m_currentAngle + m_speed > m_targetAngle;
        }
        else    //If speed is negative, check to see if we are under our target angle
        {
            return m_currentAngle + m_speed < m_targetAngle;
        }
    }

    protected override void Start()
    {
        base.Start();
        if (m_active)   //If we start active...
        {
            SwingTo(m_activeAngle); //swing to the active angle
        }
        else    //Otherwise
        {
            SwingTo(m_inactiveAngle); //Swing to the inactive angle
        }
    }

    override protected void Update()
    {
        if (m_swinging) //If we are swinging...
        {
            if (CheckAngle())   //if the next angle were to exceed the desired angle
            {
                SwingTo(m_targetAngle); //Swing to that desired angle
                m_swinging = false; //Stop swinging
            }
            else
            {
                Swing(m_speed); //Otherwise, swing by speed.
            }
        }
    }

    public override void Activate()
    {
        if (!m_active)
        {
            base.Activate();
            m_targetAngle = m_activeAngle;
            m_swinging = true;
            if (m_currentAngle > m_targetAngle) //If we are further than the desired angle...
            {
                m_speed = -1 * Math.Abs(m_activeSpeed);   //set speed to inverse
            }
            else    //If we are before the desired angle
            {
                m_speed = Math.Abs(m_activeSpeed);   //set speed to normal
            }
        }
    }

    public override void Deactivate()
    {
        if (m_active)
        {
            base.Deactivate();
            m_targetAngle = m_inactiveAngle;
            m_swinging = true;
            if (m_currentAngle > m_targetAngle) //If we are further than the desired angle...
            {
                m_speed = -1 * Math.Abs(m_inactiveSpeed);   //set speed to inverse
            }
            else    //If we are before the desired angle
            {
                m_speed = Math.Abs(m_inactiveSpeed);   //set speed to normal
            }
        }
    }
}
