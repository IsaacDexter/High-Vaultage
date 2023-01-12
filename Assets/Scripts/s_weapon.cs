using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weapon : MonoBehaviour
{
    [SerializeField] public float m_chargeCost;
    [SerializeField] protected s_hand m_hand;
    /// <summary>The collision of the player for the weapon to apply force to.</summary>
    protected Rigidbody m_rigidBody;

    virtual public void Press()
    {
        if (m_chargeCost <= m_hand.m_charge) //Check if there is enough charge to fire the weapon. If there is...
        {
            Fire();                 //...Call the weapon that it's triggers been pulled
            m_hand.m_charge -= m_chargeCost; //Reduce charge according to the weapon's cost
        }
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
