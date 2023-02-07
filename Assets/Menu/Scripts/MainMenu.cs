using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenu : s_menu
{
    public string FirstLevel;
    public GameObject Camera;
    public Canvas UI;
    public Canvas UI2;
    public Canvas UILevel;
    public GameObject Player;
    public GameObject PlayerCamera;
    public GameObject LerpScript;
    [SerializeField] private s_settings m_settings;

    // Start is called before the first frame update
    override protected void Start()
    {
        m_timeDilation = 1.0f;

        if (m_settings == null)
        {
            m_settings = GetComponentInChildren<s_settings>();
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;      //Unlock and show the cursor
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void StartGame()
    {
        UI.enabled = false;
        StartCoroutine(LerpScript.GetComponent<LerpScript>().LevelSelectLerp(UILevel));
    }

    public void OpenOptions()
    {
        m_settings.Open();
    }

    public void CloseOptions()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectLevel(int LevelNumber)
    {
        UI2.enabled = true;
        UILevel.enabled = false;
        StartCoroutine(LerpScript.GetComponent<LerpScript>().LevelSelectedLerp(Player));
    }



}
