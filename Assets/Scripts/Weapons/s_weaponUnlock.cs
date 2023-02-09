using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weaponUnlock : MonoBehaviour
{
    [Header("Weapons")]
    [Tooltip("The type of weapon to be unlocked when the player interacts with this.")]
    public s_weapon m_weapon;
    [Tooltip("Set to true to remove this weapon from the player as opposed to unlocking it.")]
    public bool m_locking = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //If the player overlaps...
        {
            s_player player = other.gameObject.transform.root.GetComponent<s_player>();
            s_weaponWheel weaponWheel = player.m_weaponWheel;
            if (weaponWheel != null)
            {
                if (!m_locking)
                {
                    weaponWheel.Unlock(m_weapon);
                }
                else
                {
                    weaponWheel.Lock(m_weapon);
                }
            }
        }
    }
}
