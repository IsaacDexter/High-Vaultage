using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] Rigidbody m_rigidBody;
    [SerializeField] Transform m_Camera;
    [SerializeField] LayerMask m_LayerMask;
    [SerializeField] float m_slideDrag;
    Vector3 m_oldTransform;
    bool m_isSliding = false;
    bool m_isGrounded;

    private void Start()
    {
        bool m_isGrounded = GetComponent<PlayerMovement>().isGrounded;
        m_oldTransform = m_Camera.transform.transform.localPosition;
    }
    private void Update()
    {
        m_isGrounded = GetComponent<PlayerMovement>().isGrounded;

        if(m_isGrounded && Input.GetKeyDown("left shift"))
        {
            m_isSliding = true;
        }
        if(Input.GetKeyUp("left shift"))
        {
            m_isSliding = false;
        }

        Slide();
    }
    private void Slide()
    {
        if(m_isSliding)
        {
            m_rigidBody.drag = m_slideDrag;
            m_Camera.transform.transform.localPosition = new Vector3(0, 0.1f, 0);
        }
        else
        {
            if (m_isGrounded)
                m_rigidBody.drag = GetComponent<PlayerMovement>().m_groundDrag;
            else
                m_rigidBody.drag = GetComponent<PlayerMovement>().m_airDrag;

            m_Camera.transform.transform.localPosition = m_oldTransform;
        }        
    }
}
