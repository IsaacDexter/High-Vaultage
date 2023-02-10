using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_sceneManager : MonoBehaviour
{
    s_levelLoader levelLoader = new s_levelLoader();
    LevelStreamer streamer = new LevelStreamer();

    [SerializeField] public GameObject player;

    //IEnumerator LoadSelectedLevel()
    //{
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(player.GetComponent<s_levelLoader>().loadingLevel, LoadSceneMode.Additive);

    //    while (!asyncLoad.isDone)
    //    {
    //        yield return null;
    //    }

    //    // Wait as we need the relative position from the triggers/ loaded level
    //    if (asyncLoad.isDone)
    //    {
    //        foreach (var obj in SceneManager.GetSceneByName("TriggersScene").GetRootGameObjects())
    //        {
    //            if (obj.name.Replace("Spawn", "") == player.GetComponent<s_levelLoader>().loadingLevel)
    //            {
    //                Debug.Log($"Spawn found {player.GetComponent<s_levelLoader>().loadingLevel}");
    //                //player.transform.position = obj.transform.position;
    //                foreach (var obj2 in SceneManager.GetSceneByName("PlayerControllerScene").GetRootGameObjects())
    //                {
    //                    if (obj2.name == "p_player")
    //                    {
    //                        Debug.Log(obj.transform.position);
    //                        //obj2.transform.position = obj.transform.position;

    //                        //obj2.GetComponent<s_player>().LoadIntoSelectedLevel(obj.transform.position);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //IEnumerator loadTriggerScene()
    //{
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TriggersScene", LoadSceneMode.Additive);

    //    while (!asyncLoad.isDone)
    //    {
    //        yield return null;
    //    }

    //    if (asyncLoad.isDone)
    //    {
    //        // Triggers loaded, we can load the level now
    //        StartCoroutine(LoadSelectedLevel());
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetSceneByName("MainMenuScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("MainMenuScene");
        }

        SceneManager.LoadSceneAsync("levelCor1", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("TriggersScene", LoadSceneMode.Additive);

        //StartCoroutine(loadTriggerScene());
    }
}
