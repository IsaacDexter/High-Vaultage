using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_spawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //If the player overlaps...
        {
            s_playerHealthManager healthManager = other.gameObject.GetComponentInParent<s_playerHealthManager>();   //Get the health manager
            if (healthManager != null)
            {
                healthManager.m_spawnPoint = this;  //Set this to be its respawn point
            }
        }
    }
}
