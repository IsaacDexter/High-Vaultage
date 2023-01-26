using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerEntered : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] int id;
    [SerializeField] string LookForTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // only trigger if 'Player' collides
        if (other.CompareTag(LookForTag))
        {
            TriggerEventManager.current.TriggerEvent(id);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // only trigger if 'Player' collides
        if (other.CompareTag(LookForTag))
        {
            TriggerEventManager.current.TriggerEventExit(id);
        }
    }
}
