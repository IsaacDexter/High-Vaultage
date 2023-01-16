using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_spawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            s_playerHealthManager healthManager = other.gameObject.GetComponentInParent<s_playerHealthManager>();
            if (healthManager != null)
            {
                healthManager.m_spawnPoints.Push(transform);
            }
        }
    }
}
