using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class s_settings : s_menu
{
    int m_resolutionX, m_resolutionY;
    FullScreenMode m_fullscreenMode;
    float m_brightness;
    public s_brightness m_postProcess;
    public s_menu m_pause;
    protected override void Start()
    {
        base.Start();
        m_timeDilation = 0.0f;
        m_key = KeyCode.Escape;

        //Default values:
        m_resolutionX = Screen.width;
        m_resolutionY = Screen.height;
        m_fullscreenMode = FullScreenMode.FullScreenWindow;
        m_brightness = 2.49f;

        //Apply();
    }

    public void Exit()
    {
        Close();
        m_pause.Open();
    }

    public void Apply()
    {
        print("Applying brightness of " + m_brightness);
        ApplyGraphics();
    }

    private void ApplyGraphics()
    {
        Screen.SetResolution(m_resolutionX, m_resolutionY, m_fullscreenMode);
        m_postProcess.SetBrightness(m_brightness);
    }

    public void SetScreenResolution(TextMeshProUGUI dropdown)
    {
        string resolution = dropdown.text;
        m_resolutionX = int.Parse(resolution.Substring(0, resolution.IndexOf('×')));
        m_resolutionY = int.Parse(resolution.Substring(resolution.IndexOf('×') + 1, resolution.Length - (resolution.IndexOf('×') + 1)));
    }

    public void SetFullscreen(TextMeshProUGUI dropdown)
    {
        string fullscreen = dropdown.text;
        switch (fullscreen)
        {
            case "Fullscreen":
                m_fullscreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case "Windowed":
                m_fullscreenMode = FullScreenMode.Windowed;
                break;
            case "Windowed Borderless":
                m_fullscreenMode = FullScreenMode.FullScreenWindow;
                break;
            default:
                break;
        }
        print(m_fullscreenMode);
    }

    public void SetBrightness(float value)
    {
        m_brightness = value;
    }
}
