using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_killVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            s_player healthManager = other.gameObject.GetComponentInParent<s_player>();
            if (healthManager != null)
            {
                healthManager.Kill();
            }
        }
    }
}
