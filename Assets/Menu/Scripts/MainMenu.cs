using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public string FirstLevel;
    public GameObject Camera;
    public Canvas UI;
    public Canvas UI2;
    public Canvas UILevel;
    public GameObject Player;
    public GameObject PlayerCamera;
    public GameObject LerpScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void StartGame()
    {
        Debug.Log("Start");


        UI.enabled = false;
        StartCoroutine(LerpScript.GetComponent<LerpScript>().LevelSelectLerp(UILevel));
    }

    public void OpenOptions()
    {
        Debug.Log("TEST EXPERIMENT");
    }

    public void CloseOptions()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }

    public void SelectLevel(int LevelNumber)
    {
        UI2.enabled = true;
        UILevel.enabled = false;
        StartCoroutine(LerpScript.GetComponent<LerpScript>().LevelSelectedLerp(Player));
    }



}
