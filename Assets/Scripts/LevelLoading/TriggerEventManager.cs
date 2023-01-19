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

    public event Action<int> onTriggerEvent;
    public void TriggerEvent(int id)
    {
        if (onTriggerEvent != null)
        {
            onTriggerEvent(id);
        }
    }
}
