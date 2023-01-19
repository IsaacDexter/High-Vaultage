using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEvent : MonoBehaviour
{
    LevelStreamer streamer;

    [Header("Data")]
    [SerializeField] int id;
    [SerializeField] string LoadLevelName;
    [SerializeField] bool LoadLevel;

    void Start()
    {
        streamer = new LevelStreamer(); 
        TriggerEventManager.current.onTriggerEvent += OnTriggerEvent;
    }
    private void OnTriggerEvent(int id)
    {
        if (SceneManager.GetSceneByName(LoadLevelName).IsValid() && LoadLevel) return;

        if (id == this.id)
        {
            Debug.Log($"Triggered {id} next; {LoadLevelName}. loader? {LoadLevel}");
            StartCoroutine(streamer.AsyncLoadScene(LoadLevelName, LoadLevel));
        }
    }
}
