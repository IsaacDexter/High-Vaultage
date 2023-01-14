using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITurret : MonoBehaviour
{
    public float rayCastRadius;
    public float targetDetectionDistance;

    private RaycastHit hitInfo;
    private bool hasDetectedEnemy = false;

    public Transform target;

    public GameObject projectile;
    public GameObject whoToTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hasDetectedEnemy = Physics.SphereCast(transform.position, rayCastRadius, transform.forward, out hitInfo, targetDetectionDistance);

        if (hasDetectedEnemy)
        {
            if (hitInfo.transform.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player Detected");
            }
            else
            {
                //LOL, IDK I just needed an else here otherwise it broke
            }    
        }
    }

    private void OnDrawGizmos()
    {
        if (hasDetectedEnemy)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawSphere(transform.position, rayCastRadius);
    }

}
