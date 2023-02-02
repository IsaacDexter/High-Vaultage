using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
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
    bool m_hdr;
    float m_renderScale;
    int m_shadowCascades;
    float m_shadowDistance;
    bool m_bloom;
    bool m_motionBlur;

    public UniversalRenderPipelineAsset m_pipeline;
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
        m_antiAliasing = m_pipeline.msaaSampleCount;
        print("2XMSAA = " + m_antiAliasing);
        m_textureRes = QualitySettings.masterTextureLimit;
        m_hdr = m_pipeline.supportsHDR;
        m_renderScale = m_pipeline.renderScale;
        m_shadowCascades = m_pipeline.shadowCascadeCount;
        m_shadowDistance = m_pipeline.shadowDistance;
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
        if (m_qualityLevel != 3)
        {
            QualitySettings.SetQualityLevel(m_qualityLevel.Value);
        }
        else
        {
            QualitySettings.vSyncCount = m_vsync;
            m_pipeline.msaaSampleCount = m_antiAliasing;
            QualitySettings.masterTextureLimit = m_textureRes;
            m_pipeline.supportsHDR = m_hdr;
            m_pipeline.renderScale = m_renderScale;
            m_pipeline.shadowCascadeCount = m_shadowCascades;
            m_pipeline.shadowDistance = m_shadowDistance;
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
        m_antiAliasing = (int)Mathf.Pow(2, value);
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
            m_qualityLevel = value;
            m_advancedSettings.SetActive(true);
        }
    }

    public void SetBrightness(float value)
    {
        m_brightness = value;
    }

    #endregion

    #region AdvancedSettings
   
    public void SetHDR(bool value)
    {
        m_hdr = value;
    }

    public void SetRenderScale(float value)
    {
        m_renderScale = value;
    }

    public void SetShadowCascades(float value)
    {
        m_shadowCascades = (int)value;
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
