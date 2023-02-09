using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_victoryZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //If the player overlaps...
        {
            s_player healthManager = other.gameObject.GetComponentInParent<s_player>();   //Get the health manager
            healthManager.OpenVictoryScreen();
        }
    }
}
