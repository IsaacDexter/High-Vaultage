using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_messager : MonoBehaviour
{
    [Tooltip("The message to send to the player.")]
    public string message;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //If the player overlaps...
        {
            s_player player = other.gameObject.GetComponentInParent<s_player>();   //Get the health manager
            if (player != null)
            {
                player.ShowMessage(message);
            }
        }
    }
}
