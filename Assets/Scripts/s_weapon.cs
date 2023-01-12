using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weapon : MonoBehaviour
{
    [SerializeField] protected int m_ammo;
    [SerializeField] protected int m_maxAmmo;
    [SerializeField] protected float m_regenDuration;
    /// <summary>A queue for regen times </summary>
    protected Queue<float> m_regenQueue = new Queue<float>();
    /// <summary>The collision of the player for the weapon to apply force to.</summary>
    protected Rigidbody m_rigidBody;

    virtual public void Press()
    {
        if (CheckAmmo())
        {
            ConsumeAmmo();
            Fire();
        }
    }
    virtual public void Release()
    {
        
    }
    virtual protected void Fire()
    {
        
    }

    protected void Regen()
    {
        if (m_ammo < m_maxAmmo)
        {
            if (Time.time >= m_regenQueue.Peek())
            {
                m_regenQueue.Dequeue();
                m_ammo++;
            }
        }
    }

    protected bool CheckAmmo()
    {
        if (m_ammo >= 1)
        {
            return true;
        }
        return false;
    }

    protected void ConsumeAmmo()
    {
        m_ammo--;
        float startTime;
        if (!m_regenQueue.TryPeek(out startTime))
        {
            startTime = Time.time;
        }
        m_regenQueue.Enqueue(startTime + m_regenDuration);
    }

    virtual protected void Update()
    {
        Regen();
    }
    protected void Start()
    {
        m_rigidBody = transform.parent.parent.GetComponent<Rigidbody>();
    }
}
