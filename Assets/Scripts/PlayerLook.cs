using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float m_sensitivityX;
    [SerializeField] float m_sensitivityY;
    [SerializeField] Transform m_camera;
    [SerializeField] Transform m_orientation;

    float m_deltaX;
    float m_deltaY;
    float m_multiplier = 0.01f;
    float m_xRotation;
    float m_yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        HandleInput();

        m_camera.transform.rotation = Quaternion.Euler(m_xRotation, m_yRotation, 0);
        m_orientation.transform.rotation = Quaternion.Euler(0, m_yRotation, 0);
    }
    private void HandleInput()
    {
        m_deltaX = Input.GetAxisRaw("Mouse X");
        m_deltaY = Input.GetAxisRaw("Mouse Y");

        m_yRotation += m_deltaX * m_sensitivityX * m_multiplier;
        m_xRotation -= m_deltaY * m_sensitivityY * m_multiplier;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);
    }
}
