using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Still_AI : MonoBehaviour
{

    [SerializeField]
    NavMeshAgent navMeshAgent;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if(navMeshAgent != null)
        {
            Debug.LogError("There isn't a nav mesh added to " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
