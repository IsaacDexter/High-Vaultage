using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_gate : s_triggerable
{
    /// <summary>A reference to the doors lamp</summary>
    [SerializeField] private s_triggerable m_lamp;
    [SerializeField] private GameObject m_door;

    private void Open()
    {
        m_lamp.Activate();
        m_door.SetActive(false);
    }

    private void Close()
    {
        m_lamp.Deactivate();
        m_door.SetActive(true);
    }

    public override void Activate()
    {
        base.Activate();
        Open();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        Close();
    }
}
