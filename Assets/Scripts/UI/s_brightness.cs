using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class s_brightness : MonoBehaviour
{
    public PostProcessProfile m_brightnessProfile;
    public PostProcessLayer m_layer;
    AutoExposure m_exposure;
    // Start is called before the first frame update
    void Start()
    {
        m_brightnessProfile.TryGetSettings(out m_exposure);
    }

    public void SetBrightness(float brightness)
    {
        m_exposure.keyValue.value = brightness;
        print("Set brightness to " + brightness);
    }
}
