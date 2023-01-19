using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] Rigidbody m_rigidBody;
    [SerializeField] Transform m_Camera;
    [SerializeField] LayerMask m_LayerMask;
    [SerializeField] float m_slideDrag = 1;
    [SerializeField] float m_slideDashPower = 8;
    Vector3 m_oldTransform;
    [HideInInspector] public bool m_isSliding = false;
    bool m_isGrounded;
    bool m_shouldBeSliding;
    [SerializeField] Transform m_body;

    private void Start()
    {
        bool m_isGrounded = GetComponent<PlayerMovement>().isGrounded;
        m_oldTransform = m_Camera.transform.transform.localPosition;
    }
    private void Update()
    {
        m_isGrounded = GetComponent<PlayerMovement>().isGrounded;

        if (Input.GetKeyDown("left shift"))
            m_shouldBeSliding = true;

        if(Input.GetKeyUp("left shift"))
        {
            m_isSliding = false;
            m_shouldBeSliding = false;
        } 
    }
    private void FixedUpdate()
    {
        if (m_shouldBeSliding)
        {
            if (m_isGrounded)
            {
                Vector3 normalized = m_rigidBody.velocity.normalized;
                m_rigidBody.AddForce(m_Camera.forward * m_slideDashPower, ForceMode.Impulse);
                m_isSliding = true;
                m_shouldBeSliding = false;
            }
        }
        Slide();
    }
    private void Slide()
    {
        if(m_isSliding)
        {
            m_body.localScale = new Vector3(1, 0.5f, 1);
            m_rigidBody.drag = m_slideDrag;
            m_Camera.transform.transform.localPosition = new Vector3(0, 0.15f, 0);
            GetComponent<PlayerMovement>().m_playerHeight = 1.0f;
        }
        else
        {
            if (m_isGrounded)
                m_rigidBody.drag = GetComponent<PlayerMovement>().m_groundDrag;
            else
                m_rigidBody.drag = m_slideDrag;

            m_Camera.transform.transform.localPosition = m_oldTransform;
            GetComponent<PlayerMovement>().m_playerHeight = 2.0f;
            m_body.localScale = new Vector3(1, 1, 1);
        }        
    }
}
