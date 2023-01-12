using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weapon : MonoBehaviour
{
    [SerializeField] public float m_chargeCost;
    /// <summary>The collision of the player for the weapon to apply force to.</summary>
    protected Rigidbody m_rigidBody;

    virtual public void Press()
    {
        Fire();
    }
    virtual public void Release()
    {
        
    }
    virtual protected void Fire()
    {
        
    }

    virtual protected void Update()
    {

    }
    protected void Start()
    {
        m_rigidBody = transform.parent.parent.GetComponent<Rigidbody>();
    }
}
