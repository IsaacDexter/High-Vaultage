using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    [SerializeField] float m_playerHeight = 2f;
    [SerializeField] float m_jumpForce = 5f;

    [SerializeField] float m_airMovementMultiplier = 0.03f;
    [SerializeField] LayerMask m_groundMask;
    public float m_groundDrag;
    public float m_airDrag;
    [SerializeField] float m_maxVelocity;

    float m_MovementMultiplier = 10f;

    float m_horizontalMovement;
    float m_verticalMovement;
    Vector3 m_moveDirection;
    Rigidbody m_rigidBody;
    [HideInInspector] public bool isGrounded;
    bool m_canJump = true;

    [SerializeField] Transform m_cameraTransform;
    Vector3 oldCameraPosition;
    Vector3 positionShift;

    [SerializeField] Transform m_orientation;

    private void Start()
    {
        oldCameraPosition = m_cameraTransform.transform.position;
        positionShift = oldCameraPosition;
        positionShift.y -= 0.5f;

        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.freezeRotation = true;
    }
    private void Update()
    {
        if (isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, m_playerHeight / 2, 0), 0.4f, m_groundMask))
        {
            m_canJump = true;
        }

        MyInput();
        ControlDrag();

        if (Input.GetKeyDown("space") && m_canJump)
        {
            Jump();
        }

        // Caps the player's maximum velocity after all other modifications
        Vector3 normalized = m_rigidBody.velocity.normalized;
        if (m_rigidBody.velocity.magnitude > m_maxVelocity)
            m_rigidBody.velocity = normalized * m_maxVelocity;

        Debug.Log(m_rigidBody.drag);
    }
    void MyInput()
    {
        m_horizontalMovement = Input.GetAxisRaw("Horizontal");
        m_verticalMovement   = Input.GetAxisRaw("Vertical");

        m_moveDirection = m_orientation.forward * m_verticalMovement + m_orientation.right * m_horizontalMovement;
    }
    void ControlDrag()
    {
        if (isGrounded)
            m_rigidBody.drag = m_groundDrag;
        else
            m_rigidBody.drag = m_airDrag;
    }
    private void FixedUpdate()
    {
        if(isGrounded)
            m_rigidBody.AddForce(m_moveDirection.normalized * moveSpeed * m_MovementMultiplier, ForceMode.Acceleration);
        else
            m_rigidBody.AddForce(m_moveDirection * moveSpeed * m_MovementMultiplier * m_airMovementMultiplier, ForceMode.Force);


    }
    private void Jump()
    {
        if (isGrounded && m_canJump)
        {
            m_rigidBody.AddForce(transform.up * m_jumpForce, ForceMode.Impulse);
        }
        else if (m_canJump)
        {
            m_canJump = false;
            m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);
            m_rigidBody.AddForce(transform.up * m_jumpForce, ForceMode.Impulse);
        }
    }
}
