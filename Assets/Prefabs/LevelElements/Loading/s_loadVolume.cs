using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_loadVolume : MonoBehaviour
{
    /// <summary>The string name of the scene to load when you pass through this volumne. Its got to be added to the build settings in order to be loaded.</summary>
    [SerializeField] private string m_sceneToLoad;
    [SerializeField] private string m_corridorToLoad;
    /// <summary>The string name of the scene to unload when you pass through this volumne. Its got to be added to the build settings and exist in order to be unloaded successfully.</summary>
    [SerializeField] private string m_sceneToUnload;
    [SerializeField] private string m_corridorToUnload;
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.tag);

        if (other.CompareTag("Player")) //When the player triggers this
        {
            s_levelLoader levelLoader = other.gameObject.GetComponentInParent<s_levelLoader>(); //Get a reference to the persistent level loader inside the player so it can set the currentlevel correctly
            Debug.Log($"player hit loadVolume {levelLoader}");

            if (levelLoader != null)    //once we have it,
            {
                //levelLoader.UnloadLevel(m_corridorToUnload);
                //levelLoader.UnloadLevel(m_sceneToUnload);   //Unload the previous level.
                //levelLoader.LoadLevel(m_corridorToLoad);
                levelLoader.LoadLevel(m_sceneToLoad);   //Load the next one. Load this last so we know which to reload on respawn.

                //Destroy(levelLoader.gameObject);    //Destroy the load volume so we no longer have to worry about loading twice.
                Destroy(gameObject);
            }
        }
    }
}
