using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    private CharacterController m_characterController;
    private Vector3 m_playerVelocity;
    private bool  m_groundedPlayer;
    

    private float m_playerSpeed = 2.0f;
    private float m_jumpHeight = 1.0f;
    private float m_gravityValue = -4.9f;

    // Start is called before the first frame update
    void Start()
    {
      m_characterController = gameObject.AddComponent<CharacterController>(); 
      Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementController();
    }

    void PlayerMovementController()
    {
        m_groundedPlayer = m_characterController.isGrounded;
        if (m_groundedPlayer && m_playerVelocity.y < 0)
        {
            m_playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        m_characterController.Move(move * Time.deltaTime * m_playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (Input.GetButtonDown("Jump"))
        {
            m_playerVelocity.y += Mathf.Sqrt(m_jumpHeight * -3.0f * m_gravityValue);
        }

        m_playerVelocity.y += m_gravityValue * Time.deltaTime;
        m_characterController.Move(m_playerVelocity * Time.deltaTime);
    }

    
}
