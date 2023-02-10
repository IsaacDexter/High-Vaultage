using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class s_loadFirst : MonoBehaviour
{
    LevelStreamer streamer = new LevelStreamer();
    [SerializeField] public string loadLevelName;
    [SerializeField] public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.tag} {other.name}");

        if (other.CompareTag("Player")) //When the player triggers this
        {
            s_levelLoader levelLoader = other.gameObject.GetComponentInParent<s_levelLoader>(); //Get a reference to the persistent level loader inside the player so it can set the currentlevel correctly
            Debug.Log($"player hit loadVolume {levelLoader}");

            if (levelLoader != null)    //once we have it,
            {
                SceneManager.LoadScene("PlayerControllerScene");
                player.GetComponent<s_levelLoader>().loadingLevel = loadLevelName;

                gameObject.SetActive(false); //Destroy the load volume so we no longer have to worry about loading twice.
            }
        }
    }
}
