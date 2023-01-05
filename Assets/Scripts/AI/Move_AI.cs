using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move_AI : MonoBehaviour
{
    [SerializeField]
    Transform _destination;
    NavMeshAgent _navMeshAgent;


    private void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>(); //Gets the Nav Mesh

        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attactched to " + gameObject.name);

        }

        else
        {
            SetDestination();
        }
    }

    private void SetDestination() //Sets the Navigation Destination
    {
        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }


    }
}
