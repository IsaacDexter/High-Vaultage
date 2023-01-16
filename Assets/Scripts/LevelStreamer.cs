using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStreamer 
{
    public IEnumerator AsyncLoadScene(string scene, bool loadLevel)
    { 
        if (loadLevel)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        else if (!loadLevel)
        {
            AsyncOperation asyncDeload = SceneManager.UnloadSceneAsync(scene);

            while (!asyncDeload.isDone)
            {
                yield return null;
            }
        }

    }
}
