using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Rigidbody m_rigidBody;
    public int dashForce;
    bool m_onCooldown = false;
    [SerializeField] float m_cooldown;
    [SerializeField] Transform m_orientation;
    float m_lastCoolDown = 0;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.freezeRotation = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left shift") && !m_onCooldown)
        {
            Dash();
            Debug.Log("Dashed!");
            m_onCooldown = true;
            m_lastCoolDown = m_cooldown;
        }

        if (m_lastCoolDown > 0)
        {
            m_lastCoolDown -= Time.deltaTime;
        }
        else
        {
            m_onCooldown = false;
            m_lastCoolDown = 0;
        }
    }
    void Dash()
    {
        m_rigidBody.AddForce(m_orientation.forward * dashForce, ForceMode.Impulse);
    }
}
