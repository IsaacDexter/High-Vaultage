using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public CharacterController m_characterController;
    
    public  float m_playerSpeed    =  2.0f;
    public  float m_gravityValue   = -15.0f;
    public  float m_jumpHeight     =  8.0f;

    public Transform m_groundCheck;
    private float    m_groundDistance = 0.4f;
    public LayerMask m_groundMask;

    private Vector3 m_playerVelocity;
    private bool    m_isGrounded;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundMask);
        if(m_isGrounded && m_playerVelocity.y < 0)
        {
            m_playerVelocity.y = -0.1f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        m_characterController.Move(move * m_playerSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && m_isGrounded)
        {
            m_playerVelocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_gravityValue);
        }

        m_playerVelocity.y += m_gravityValue * Time.deltaTime;
        m_characterController.Move(m_playerVelocity * Time.deltaTime);
    }
}
