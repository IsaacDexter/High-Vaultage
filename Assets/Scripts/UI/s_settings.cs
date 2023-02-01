using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Rendering;
using TMPro;

public class s_settings : s_menu
{
    int m_resolutionX, m_resolutionY;
    FullScreenMode m_fullscreenMode;
    float m_brightness;
    int m_vsync;
    int m_antiAliasing;
    int? m_qualityLevel;
    int m_textureRes;
    ShadowQuality m_shadowQuality;
    ShadowResolution m_shadowResolution;
    int m_shadowCascades;
    float m_shadowDistance;
    bool m_bloom;
    bool m_motionBlur;

    public s_brightness m_postProcess;
    public s_menu m_pause;
    [SerializeField] private GameObject m_advancedSettings;
    protected override void Start()
    {
        base.Start();
        m_timeDilation = 0.0f;
        m_key = KeyCode.Escape;

        //Default values:
        m_resolutionX = Screen.width;
        m_resolutionY = Screen.height;
        m_fullscreenMode = FullScreenMode.FullScreenWindow;
        m_brightness = 0.0f;
        m_qualityLevel = QualitySettings.GetQualityLevel();
        m_vsync = QualitySettings.vSyncCount;
        m_antiAliasing = QualitySettings.antiAliasing;
        m_textureRes = QualitySettings.masterTextureLimit;
        m_shadowQuality = QualitySettings.shadows;
        m_shadowResolution = QualitySettings.shadowResolution;
        m_shadowCascades = QualitySettings.shadowCascades;
        m_shadowDistance = QualitySettings.shadowDistance;
        m_bloom = false;
        m_motionBlur = false;

        //Apply();
    }

    public void Exit()
    {
        Close();
        m_pause.Open();
    }

    public void Apply()
    {
        ApplyGraphics();
    }

    private void ApplyGraphics()
    {
        Screen.SetResolution(m_resolutionX, m_resolutionY, m_fullscreenMode);
        m_postProcess.SetGain(m_brightness);
        m_postProcess.SetBloom(m_bloom);
        m_postProcess.SetMotionBlur(m_motionBlur);
        if (m_qualityLevel != null)
        {
            QualitySettings.SetQualityLevel(m_qualityLevel.Value);
        }
        else
        {
            QualitySettings.vSyncCount = m_vsync;
            QualitySettings.antiAliasing = m_antiAliasing;
            QualitySettings.masterTextureLimit = m_textureRes;
            QualitySettings.shadows = m_shadowQuality;
            QualitySettings.shadowResolution = m_shadowResolution;
            QualitySettings.shadowCascades = m_shadowCascades;
            QualitySettings.shadowDistance = m_shadowDistance;
            
        }
    }

    #region WindowSettings

    public void SetScreenResolution(TextMeshProUGUI dropdown)
    {
        string resolution = dropdown.text;
        m_resolutionX = int.Parse(resolution.Substring(0, resolution.IndexOf('×')));
        m_resolutionY = int.Parse(resolution.Substring(resolution.IndexOf('×') + 1, resolution.Length - (resolution.IndexOf('×') + 1)));
    }

    public void SetVsync(bool value)
    {
        if (value)
        {
            m_vsync = 1;
        }
        else 
        {
            m_vsync = 0;
        }
    }


    public void SetFullscreen(int value)
    {
        switch (value)
        {
            case 0:
                m_fullscreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                m_fullscreenMode = FullScreenMode.Windowed;
                break;
            case 2:
                m_fullscreenMode = FullScreenMode.FullScreenWindow;
                break;
            default:
                break;
        }
    }

    #endregion

    #region BasicSettings

    public void SetAntiAliasing(int value)
    {
        switch (value)
        {
            case 0:
                m_antiAliasing = 0;
                break;
            default:
                m_antiAliasing = 2 ^ value;
                break;
        }
    }

    public void SetQualityLevel(int value)
    {
        if (value < 3)
        {
            m_qualityLevel = value;
            m_advancedSettings.SetActive(false);
        }
        else
        {
            m_qualityLevel = null;
            m_advancedSettings.SetActive(true);
        }
    }

    public void SetBrightness(float value)
    {
        m_brightness = value;
    }

    #endregion

    #region AdvancedSettings

    public void SetRealTimeShadows(int value)
    {
        switch (value)
        {
            case 0:
                m_shadowQuality = ShadowQuality.Disable;
                break;
            case 1:
                m_shadowQuality = ShadowQuality.HardOnly;
                break;
            case 2:
                m_shadowQuality = ShadowQuality.All;
                break;
            default:
                break;
        }
    }
    
    public void SetShadowResolution(int value)
    {
        switch (value)
        {
            case 0:
                m_shadowResolution = ShadowResolution.Low;
                break;
            case 1:
                m_shadowResolution = ShadowResolution.Medium;
                break;
            case 2:
                m_shadowResolution = ShadowResolution.High;
                break;
            case 3:
                m_shadowResolution = ShadowResolution.VeryHigh;
                break;
            default:
                break;
        }
    }
    public void SetShadowCascades(int value)
    {
        switch (value)
        {
            case 0:
                m_shadowCascades = 0;
                break;
            default:
                m_shadowCascades = 2 ^ value;
                break;
        }
    }

    public void SetShadowDistance(float value)
    {
        m_shadowDistance = value;
    }

    public void SetBloom(bool value)
    {
        m_bloom = value;
    }
    
    public void SetMotionBlur(bool value)
    {
        m_motionBlur = value;
    }
    public void SetTextureQuality(int value)
    {
        m_textureRes = value;
    }

    #endregion

}
