using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallRun : MonoBehaviour
{
    [SerializeField] Transform m_orientation;
    [SerializeField] float m_wallDistance = .6f;
    [SerializeField] float m_minJumpHeight = 1.5f;
    [SerializeField] float m_wallRunGravity;
    [SerializeField] float m_wallRunJumpForce;
    
    bool m_wallOnLeft = false;
    bool m_wallOnRight = false;
    RaycastHit m_leftWallHit;
    RaycastHit m_rightWallHit;

    bool check;
    float m_baseFOV;

    private Rigidbody m_rigidBody;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    bool CanWallRun()
    {
        return !(Physics.Raycast(transform.position, Vector3.down, m_minJumpHeight));
    }

    void CheckWalls()
    {
        m_wallOnLeft  = Physics.Raycast(transform.position, -m_orientation.right, out m_leftWallHit, m_wallDistance, LayerMask.GetMask("RunnableWall"));
        m_wallOnRight = Physics.Raycast(transform.position, m_orientation.right, out m_rightWallHit, m_wallDistance, LayerMask.GetMask("RunnableWall"));
    }

    private void Update()
    {
        CheckWalls();

        if (CanWallRun())
        {
           if(m_wallOnLeft || m_wallOnRight)
            {
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    void StartWallRun()
    {
        if (!check)
        {
            m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);
            check = true;
        }

        m_rigidBody.useGravity = false;
        m_rigidBody.AddForce(Vector3.down * 0.1f, ForceMode.Force);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)                //The action has started i.e. the key has gone down
        {
            if (m_wallOnLeft)
            {
                Vector3 jumpDirection = transform.up + m_leftWallHit.normal;
                m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);
                m_rigidBody.AddForce(jumpDirection * m_wallRunJumpForce, ForceMode.Impulse);
            }
            else if (m_wallOnRight)
            {
                Vector3 jumpDirection = transform.up + m_rightWallHit.normal;
                m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);
                m_rigidBody.AddForce(jumpDirection * m_wallRunJumpForce, ForceMode.Impulse);
            }
        }
    }

    void StopWallRun()
    {
        m_rigidBody.useGravity = true;
        check = false;
    }
}
