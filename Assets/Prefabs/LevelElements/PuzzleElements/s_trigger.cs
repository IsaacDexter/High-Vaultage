using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_trigger : MonoBehaviour
{
    /// <summary>If the trigger is pressed or triggered or not</summary>
    protected bool m_triggered = false;


    /// <summary>The game objects to activate when this trigger is pressed or triggered</summary>
    [SerializeField] protected s_triggerable[] m_outputs;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        
    }

    /// <summary>Activate all of the outputs</summary>
    virtual public void Trigger()
    {
        if (!m_triggered)
        {
            m_triggered = true;
            foreach (s_triggerable output in m_outputs)
            {
                output.Activate();
            }
        }
    }

    /// <summary>Deactivate all of the outputs</summary>
    virtual public void Detrigger()
    {
        if (m_triggered)
        {
            m_triggered = false;
            foreach (s_triggerable output in m_outputs)
            {
                output.Deactivate();
            }
        }
    }
}
