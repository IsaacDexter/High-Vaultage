using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_victoryMenu : s_menu
{
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
}
