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
    [Tooltip("Whether or not to alert the player to the collection of this weapon")]
    public bool m_message = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //If the player overlaps...
        {
            s_player player = other.gameObject.transform.root.GetComponent<s_player>();

            if (!m_locking)
            {
                player.Unlock(m_weapon);
                if (m_message)
                {
                    player.ShowMessage(m_weapon.GetType().Name.Replace("s_","") + " unlocked!");
                    m_message = false;
                }
            }
            else
            {
                player.Lock(m_weapon);
                if (m_message)
                {
                    player.ShowMessage(m_weapon.GetType().Name.Replace("s_", "") + " locked!");
                    m_message=false;
                }
            }

        }
    }
}
