using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_triggerable : MonoBehaviour
{
    [SerializeField] protected bool m_active = false;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        SetActive(m_active);
    }

    // Update is called once per frame
    virtual protected void Update()
    {

    }

    public virtual void Activate()
    {
        if (m_active)
        {
            return;
        }
        m_active = true;
    }

    public virtual void Deactivate()
    {
        if (!m_active)
        {
            return;
        }
        m_active = false;
    }

    public void SetActive(bool active)
    {
        if (active)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }
}
