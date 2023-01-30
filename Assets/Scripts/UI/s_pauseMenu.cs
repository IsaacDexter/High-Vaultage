using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_pauseMenu : s_menu
{
    public void Quit()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
}
