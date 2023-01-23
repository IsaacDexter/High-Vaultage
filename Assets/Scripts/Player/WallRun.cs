using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [SerializeField] Transform m_orientation;
    [SerializeField] float m_wallDistance = 0.5f;
    [SerializeField] float m_minJumpHeight = 1.5f;
    

    bool m_wallOnLeft = false;
    bool m_wallOnRight = false;

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, m_minJumpHeight);
    }
    void CheckWalls()
    {
        m_wallOnLeft  = Physics.Raycast(transform.position, -m_orientation.right, m_wallDistance, LayerMask.GetMask("RunnableWall"));
        m_wallOnRight = Physics.Raycast(transform.position, m_orientation.right, m_wallDistance, LayerMask.GetMask("RunnableWall"));
    }
    private void Update()
    {
        CheckWalls();
        if(CanWallRun())
        {
            if (m_wallOnLeft)
            {
                Debug.Log("Holding on left wall");
            }
            else if (m_wallOnRight)
            {
                Debug.Log("Holding on right wall");
            }
        }
    }
}
