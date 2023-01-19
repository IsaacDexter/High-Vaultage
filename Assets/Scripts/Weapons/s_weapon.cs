using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weapon : MonoBehaviour
{
    /// <summary>The cost of the weapon to fire per shot. For charging weapons, the cost each second.</summary>
    [SerializeField] public float m_chargeCost;
    /// <summary>The hand that is holding the weapon, used to reduce the charge as the weapon fires.</summary>
    protected s_hand m_hand;
    /// <summary>The collision of the player for the weapon to apply force to.</summary>
    protected Rigidbody m_rigidBody;
    /// <summary>The transform of the camera to avoid a sea of .parents</summary>
    protected Transform m_camera;
    /// <summary>The transform of the camera to avoid a sea of .parents</summary>
    protected Transform m_player;

    /// <summary>Called when the trigger is pulled. Calculates if there is enough ammo to fire, then fires, if possible.</summary>
    virtual public void Press()
    {
        Fire(); //...Call the weapon that it's triggers been pulled
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
        m_hand = GetComponentInParent<s_hand>();            //Get the hand from the player, as it will be its parent, always.
        m_camera = m_hand.transform.parent;                 //Get the camera's transform, the hands's parent's transform
        m_player = m_camera.parent;                         //Get the parent of the camera, i.e the player's transform
        m_rigidBody = m_player.GetComponent<Rigidbody>();   //Binds the player's rigidbody so this weapon can affect it with force
        
        m_hand.m_regening = true;   //Make sure the hand is initialising ammo, in case the player attempted to switch while charging a weapon.
    }
}
