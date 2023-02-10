using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Goes in player
public class s_levelLoader : MonoBehaviour
{
    LevelStreamer streamer = new LevelStreamer();
    string m_currentLevel;

    [SerializeField] public string loadingLevel;

    void Start()
    {

    }

    /// <summary>loads scene, regardless of whether or not it already exists, and sets m_currentLevel to scene. </summary>
    /// <param name="scene">The string name of the scene to load. The scene needs to be in build settings or it can't be loaded (File > Build Settings).</param>
    /// <returns>Whether the scene was loaded successfully. always true</returns>
    public bool LoadLevel(string scene)
    {
        //Doesn't need to check if scene already exists as there's a very very brief overlap.

        // Calls AsyncLoadScene from the LevelStreamer class to load a scene additevly using a coroutine to run level loading in a seperate thread to decrease load times
        // The second variable is to declare whether or not it loads or unloads, true being load false being unload
        StartCoroutine(streamer.AsyncLoadScene(scene, true));
        m_currentLevel = scene;
        return true;
    }

    /// <summary>Unloads scene, so long as it exists, and sets m_currentLevel to nothing. </summary>
    /// <param name="scene">The string name of the scene to unload. The scene needs to be in build settings or it can't be loaded (File > Build Settings). It also needs to exist, or the function will return false</param>
    /// <returns>Whether the scene was unloaded successfully.</returns>
    public bool UnloadLevel(string scene)
    {
        //Check to see if scene exists before 
        if (!SceneManager.GetSceneByName(scene).IsValid()) return false; // Scene doesnt exists! Cannot unload it.

        // Calls AsyncLoadScene from the LevelStreamer class to unload a scene using a coroutine to run level unloading in a seperate thread to decrease times
        // The second variable is to declare whether or not it loads or unloads, true being load false being unload
        StartCoroutine(streamer.AsyncLoadScene(scene, false));
        m_currentLevel = string.Empty;
        return true;
    }

    /// <summary>Unloads the current scene and then loads it.</summary>
    /// <returns>Whether the scene was unloaded successfully (regardless of it loading successfully)</returns>
    public bool ReloadLevel()
    {
        bool successful;
        successful = UnloadLevel(m_currentLevel);
        LoadLevel(m_currentLevel);
        return successful;
    }
}
