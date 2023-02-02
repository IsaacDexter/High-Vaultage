using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    private float m_mouseX, m_mouseY;
    private float xRotation = 0f;
    public float m_mouseSensitivity = 100f;
    public Transform m_playerBody;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity * Time.deltaTime;
        m_mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.deltaTime;
        xRotation -= m_mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        m_playerBody.Rotate(Vector3.up * m_mouseX);
    }
}
