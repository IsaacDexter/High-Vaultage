using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_pauseMenu : s_menu
{
    public s_menu m_settings;

    protected override void Start()
    {
        m_timeDilation = 0.0f;
        m_key = KeyCode.Escape;
        base.Start();
    }
    public void Quit()
    {
        m_open = false;  //Update check bool
        Time.timeScale = 1f;        //Speed time up to the normal amount
        gameObject.SetActive(false);
        m_player.GetComponent<s_player>().m_acceptedInput = KeyCode.None;
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

    public void OpenSettings()
    {
        Close();
        m_settings.Open();
    }
}
