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
        StartCoroutine(Animation1());
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
        StartCoroutine(ExitMenuAnimation());
    }


    IEnumerator Animation1()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        for (int i = 0; i < 40; i++)
        {
            Camera.transform.position = Camera.transform.position - new Vector3(-0.01f, 0, 0);
            Debug.Log("Moving");
            yield return new WaitForSeconds(.01f);
        }

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(.5f);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        StartCoroutine(Animation2());
    }


    IEnumerator Animation2()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        for (int i = 0; i < 160; i++)
        {
            Camera.transform.position = Camera.transform.position - new Vector3(0, 0, -0.01f);
            Debug.Log("Moving");
            yield return new WaitForSeconds(.01f);
        }

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(.5f);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        StartCoroutine(Animation3());

    }

    IEnumerator Animation3()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        for (int i = 0; i < 40; i++)
        {
            Camera.transform.position = Camera.transform.position - new Vector3(+0.01f, 0, 0);
            Debug.Log("Moving");
            yield return new WaitForSeconds(.01f);
        }

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(.5f);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        UILevel.enabled = true;

    }


    IEnumerator ExitMenuAnimation()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        for (int i = 0; i < 40; i++)
        {
            Camera.transform.position = Camera.transform.position - new Vector3(-0.01f, 0, 0);
            Debug.Log("Moving");
            yield return new WaitForSeconds(.01f);
        }

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(.5f);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        Camera.SetActive(false);
        Player.GetComponent<PlayerMovement>().enabled = true;
        Player.GetComponent<PlayerLook>().enabled = true;
        Player.GetComponent<PlayerDash>().enabled = true;
        PlayerCamera.SetActive(true);
    }


}
