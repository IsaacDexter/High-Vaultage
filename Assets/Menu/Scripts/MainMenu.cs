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
    [SerializeField] List<GameObject> listOfLights = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TurnOnLights(List<GameObject> listOfLights)
    {
        yield return new WaitForSeconds(3);
        foreach (GameObject currentLight in listOfLights)
        {
            yield return new WaitForSeconds(0.5f);

            currentLight.GetComponent<Light>().enabled = true;
        }
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


    public void TurnOnLightsN()
    {
        Debug.Log("Called turn on lights");

        foreach(GameObject currentLight in listOfLights)
        {
            Debug.Log(currentLight.name);
        }
    }

    public void SelectLevel(int LevelNumber)
    {
        UI2.enabled = true;
        UILevel.enabled = false;
        StartCoroutine(LerpScript.GetComponent<LerpScript>().LevelSelectedLerp(Player));

        StartCoroutine(TurnOnLights(listOfLights));
    }
}
