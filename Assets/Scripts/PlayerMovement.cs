using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    [SerializeField] float m_playerHeight = 2f;
    [SerializeField] float m_jumpForce = 5f;
    [SerializeField] float m_airMovementMultiplier = 0.04f;
    [SerializeField] LayerMask m_groundMask;

    float m_MovementMultiplier = 10f;
    float m_groundDrag = 6f;
    float m_airDrag = 2f;

    float m_horizontalMovement;
    float m_verticalMovement;
    Vector3 m_moveDirection;
    Rigidbody m_rigidBody;
    bool m_isGrounded;

    [SerializeField] Transform m_orientation;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.freezeRotation = true;
    }
    private void Update()
    {
        m_isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, m_playerHeight / 2, 0), 0.4f, m_groundMask);
        MyInput();
        ControlDrag();

        if (Input.GetKeyDown("space") && m_isGrounded)
        {
            Jump();
        }
    }
    void MyInput()
    {
        m_horizontalMovement = Input.GetAxisRaw("Horizontal");
        m_verticalMovement   = Input.GetAxisRaw("Vertical");

        m_moveDirection = m_orientation.forward * m_verticalMovement + m_orientation.right * m_horizontalMovement;
    }
    void ControlDrag()
    {
        if (m_isGrounded)
            m_rigidBody.drag = m_groundDrag;
        else
            m_rigidBody.drag = m_airDrag;
    }
    private void FixedUpdate()
    {
        if(m_isGrounded)
            m_rigidBody.AddForce(m_moveDirection.normalized * moveSpeed * m_MovementMultiplier, ForceMode.Acceleration);
        else
            m_rigidBody.AddForce(m_moveDirection * moveSpeed * m_MovementMultiplier * m_airMovementMultiplier, ForceMode.Force);

       
    }
    private void Jump()
    {
        if(m_isGrounded)
            m_rigidBody.AddForce(transform.up * m_jumpForce, ForceMode.Impulse);
    }
}
