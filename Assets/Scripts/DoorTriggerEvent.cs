using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerEvent : MonoBehaviour
{ 

    [Header("Data")]
    [SerializeField] int id;
    [SerializeField] Transform door;
    [SerializeField] Animator animator;

    void Start()
    {
        TriggerEventManager.current.onTriggerEventDoor += OnTriggerEvent;
        TriggerEventManager.current.onTriggerEventExit += OntTriggerEventExit;
    }
    private void OnTriggerEvent(int id)
    {
        if (id == this.id)
        {
            Debug.Log($"Trigger Event {id}");
            animator.SetBool("approach", true);
        }
    }

    private void OntTriggerEventExit(int id)
    {
        if (id == this.id)
        {
            Debug.Log($"Trigger Event Exit {id}");
            animator.SetBool("approach", false);
        }
    }
}
