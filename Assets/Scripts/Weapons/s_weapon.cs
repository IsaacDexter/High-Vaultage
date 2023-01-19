using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weapon : MonoBehaviour
{
    /// <summary>The cost of the weapon to fire per shot. For charging weapons, the cost each second.</summary>
    [SerializeField] public float m_chargeCost;
    /// <summary>The hand that is holding the weapon, used to reduce the charge as the weapon fires.</summary>
    public s_hand m_hand;
    /// <summary>The collision of the player for the weapon to apply force to.</summary>
    protected Rigidbody m_rigidBody;
    /// <summary>The transform of the camera to avoid a sea of .parents</summary>
    protected Transform m_camera;
    /// <summary>The transform of the camera to avoid a sea of .parents</summary>
    protected Transform m_player;

    /// <summary>Called when the trigger is pulled. Calculates if there is enough ammo to fire, then fires, if possible.</summary>
    virtual public void Press()
    {
        Fire();                             //...Call the weapon that it's triggers been pulled
    }
    /// <summary>Called when the trigger is released. For charge weapons, this will stop charging and fire the weapon.</summary>
    virtual public void Release()
    {
        
    }
    /// <summary>All code for the weapon firing will sit within this function.</summary>
    virtual protected void Fire()
    {
        
    }

    virtual protected void Update()
    {

    }
    protected void Start()
    {
        m_camera = transform.parent.parent;
        m_player = m_camera.parent;
        m_rigidBody = m_player.GetComponent<Rigidbody>();    //Binds the parent's rigidbody so this weapon can affect it with force
    }
}
