using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string SCENE;
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SCENE, LoadSceneMode.Additive);
    }
}
