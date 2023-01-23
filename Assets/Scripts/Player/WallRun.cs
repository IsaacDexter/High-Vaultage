using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [SerializeField] Transform m_orientation;
    [SerializeField] float m_wallDistance = 1f;
    [SerializeField] float m_minJumpHeight = 1.5f;
    
    bool m_wallOnLeft = false;
    bool m_wallOnRight = false;
    RaycastHit m_leftWallHit;
    RaycastHit m_rightWallHit;

    private Rigidbody m_rigidBody;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, m_minJumpHeight);
    }

    void CheckWalls()
    {
        m_wallOnLeft  = Physics.Raycast(transform.position, -m_orientation.right, out m_leftWallHit, m_wallDistance);
        m_wallOnRight = Physics.Raycast(transform.position, m_orientation.right, out m_rightWallHit, m_wallDistance);
    }

    private void Update()
    {
        if(CanWallRun())
        {
            CheckWalls();

            if (m_wallOnLeft || m_wallOnRight)
            {
                StartWallRun();
            }
            else if(!m_wallOnLeft && !m_wallOnRight)
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
        m_rigidBody.useGravity = false;
        m_rigidBody.AddForce(Vector3.down, ForceMode.Force);

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (m_wallOnLeft)
        //    {
        //        Vector3 jumpDirection = transform.up + m_leftWallHit.normal;
        //        m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);
        //        m_rigidBody.AddForce(jumpDirection * 50, ForceMode.Impulse);
        //    }
        //    else if(m_wallOnRight)
        //    {
        //        Vector3 jumpDirection = transform.up + m_rightWallHit.normal;
        //        m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);
        //        m_rigidBody.AddForce(jumpDirection * 50, ForceMode.Impulse);
        //    }
        //}
    }

    void StopWallRun()
    {
        m_rigidBody.useGravity = true;
    }
}
