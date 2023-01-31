using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class s_brightness : MonoBehaviour
{
    public Volume m_volume;
    private VolumeProfile m_profile;
    private LiftGammaGain m_liftGammaGain;
    private void Start()
    {
        m_volume = GetComponent<Volume>();
        m_profile = m_volume.profile;

        if (!m_profile.TryGet<LiftGammaGain>(out var lgg))
        {
            lgg = m_profile.Add<LiftGammaGain>(false);
        }
        m_liftGammaGain = lgg;
        m_liftGammaGain.gain.Override(new Vector4(1f, 1f, 1f, 0.0f));
    }

    public void SetGain(float gain)
    {
        m_liftGammaGain.gain.Override(new Vector4(1f, 1f, 1f, gain));
    }

    //public PostProcessProfile m_brightnessProfile;
    //public PostProcessLayer m_layer;
    //AutoExposure m_exposure;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    m_brightnessProfile.TryGetSettings(out m_exposure);
    //}

    //public void SetBrightness(float brightness)
    //{
    //    m_exposure.keyValue.value = brightness;
    //    print("Set brightness to " + brightness);
    //}
}
