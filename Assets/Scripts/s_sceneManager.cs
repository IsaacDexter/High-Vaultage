using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_sceneManager : MonoBehaviour
{
    s_levelLoader levelLoader = new s_levelLoader();
    LevelStreamer streamer = new LevelStreamer();

    [SerializeField] public GameObject player;

    IEnumerator loadTriggerScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TriggersScene", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (asyncLoad.isDone)
        {
            foreach (var obj in SceneManager.GetSceneByName("TriggersScene").GetRootGameObjects())
            {
                if (obj.name.Replace("Spawn", "") == player.GetComponent<s_levelLoader>().loadingLevel)
                {
                    Debug.Log($"Spawn found {player.GetComponent<s_levelLoader>().loadingLevel}");
                    //player.transform.position = obj.transform.position;
                    foreach (var obj2 in SceneManager.GetSceneByName("PlayerControllerScene").GetRootGameObjects())
                    {
                        if (obj2.name == "p_player")
                        {
                            Debug.Log("Got player");    
                            obj2.gameObject.transform.position = obj.transform.position;
                        }
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetSceneByName("MainMenuScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("MainMenuScene");
        }

        SceneManager.LoadSceneAsync(player.GetComponent<s_levelLoader>().loadingLevel, LoadSceneMode.Additive);
        StartCoroutine(loadTriggerScene());
    }
}
