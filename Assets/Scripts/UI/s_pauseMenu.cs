using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_pauseMenu : s_menu
{
    public s_menu m_settings;

    protected override void Start()
    {
        m_timeDilation = 0.0f;
        base.Start();
    }
    public void Quit()
    {
        m_open = false;  //Update check bool
        Time.timeScale = 1f;        //Speed time up to the normal amount
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

    public void Resume()
    {
        m_player.GetComponent<s_player>().TogglePause();
    }

    public override void Open()
    {
        m_settings.Close();
        base.Open();
    }

    public void OpenSettings()
    {
        Close();
        m_settings.Open();
    }
}
