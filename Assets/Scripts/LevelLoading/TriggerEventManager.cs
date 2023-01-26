using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventManager : MonoBehaviour
{
    public static TriggerEventManager current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> onTriggerEventDoor;
    public void TriggerEvent(int id)
    {
        if (onTriggerEventDoor != null)
        {
            Debug.Log("not null");
            onTriggerEventDoor(id);
        }
    }

    public event Action<int> onTriggerEventExit;
    public void TriggerEventExit(int id)
    {
        if (onTriggerEventExit != null)
        {
            onTriggerEventExit(id);
        }
    }
}
