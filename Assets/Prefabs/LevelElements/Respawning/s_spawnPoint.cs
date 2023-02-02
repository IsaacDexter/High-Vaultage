using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_spawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //If the player overlaps...
        {
            s_player healthManager = other.gameObject.GetComponentInParent<s_player>();   //Get the health manager
            if (healthManager != null)
            {
                healthManager.m_spawnPoint = gameObject.transform;  //Set this to be its respawn point
            }
        }
    }
}
