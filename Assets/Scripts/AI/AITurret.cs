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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
