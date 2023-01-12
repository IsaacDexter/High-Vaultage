using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float rayCastRadius;
    public float targetDetectionDistance;

    private RaycastHit hitInfo;
    private bool hasDetectedEnemy = false;

    public Transform target;

    public GameObject projectile;
    public GameObject whoToTarget;

    [SerializeField] private float m_firingForce = 20f;
    [SerializeField] private float shootTime;
    [SerializeField] private float randomShootTime;
    private float shootTimer;

    private void Start()
    {
       
        Debug.Log(transform.position);
    }


    
    public void CheckForTarget()
    {
        hasDetectedEnemy = Physics.SphereCast(transform.position, rayCastRadius, transform.forward, out hitInfo, targetDetectionDistance);
        hasDetectedEnemy = Physics.SphereCast(new Ray(transform.position, Vector3.forward), rayCastRadius, out hitInfo, targetDetectionDistance);


        if (hasDetectedEnemy)
        {
            if (hitInfo.transform.gameObject.CompareTag("Player"))
            {
                Debug.Log("Detected Player");

                transform.LookAt(target);

                Update();
            }
            else
            {
                Debug.Log("No Player Detected");
            }
        }
    }

    // OverlapShere method; might be laggy
    //public void CheckForTarget()
    //{
    //    Collider[] hitObjects = Physics.OverlapSphere(transform.position, rayCastRadius); 

    //    foreach(Collider hitCollider in hitObjects)
    //    {
    //        //Debug.Log(hitCollider.gameObject.tag);
    //        if (hitCollider.gameObject.CompareTag("Player"))
    //        {
    //            Debug.Log("Player Entered");
    //        }
    //    }

    //}

    // Magnitude check method
    //public void CheckForTarget()
    //{
    //    float playerDistance = (transform.position - whoToTarget.transform.position).magnitude;
    //    Debug.Log((transform.position - whoToTarget.transform.position).magnitude);

    //    if (playerDistance <= targetDetectionDistance)
    //    {
    //        transform.LookAt(whoToTarget.transform.position);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        CheckForTarget();


    }


    void ResetShootTimer()
    {
        shootTimer = 3f; 
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

        Gizmos.DrawCube(new Vector3(0f, 0f, targetDetectionDistance / 2f), new Vector3(rayCastRadius, rayCastRadius, targetDetectionDistance));
        Gizmos.DrawSphere(transform.position, rayCastRadius);
    }
}
