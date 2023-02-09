using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_sceneManager : MonoBehaviour
{
    s_levelLoader levelLoader = new s_levelLoader();
    LevelStreamer streamer = new LevelStreamer();

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start");
        //levelLoader.LoadLevel("levelCor1");
        //levelLoader.LoadLevel("TriggersScene");
        StartCoroutine(streamer.AsyncLoadScene("TriggersScene", true));
        StartCoroutine(streamer.AsyncLoadScene("levelCor1", true));
    }
}
