using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class s_brightness : MonoBehaviour
{
    [Tooltip("The global volume applying the post processes. Can be left blank.")]
    public Volume m_volume;
    [Tooltip("The global volume applying the post processes' profile, which is changed permanently.")]
    private VolumeProfile m_profile;
    [Tooltip("The profiles lift gamma gain overrides, which can be used to adjust 'brightness' of the scene.")]
    private LiftGammaGain m_liftGammaGain;
    private void Start()
    {
        //Get the volume tied to this component, if not set manually.
        if (m_volume == null)
        {
            m_volume = GetComponent<Volume>();
        }
        m_profile = m_volume.sharedProfile; //Get the shared profile, which means it will edit the profile file as opposed to a local copy

        if (!m_profile.TryGet<LiftGammaGain>(out var lgg))  //Attempt to get the lift gamma gain. This will make a new one if no override has been set.
        {
            lgg = m_profile.Add<LiftGammaGain>(false);
        }
        m_liftGammaGain = lgg;
    }

    /// <summary>Set's the shared profile's gain's alpha component, which will increase the brightness of the scene. </summary>
    /// <param name="gain">The gain to apply. Advised to clamp above -1 to stop pitch black.</param>
    public void SetGain(float gain)
    {
        m_liftGammaGain.gain.Override(new Vector4(1f, 1f, 1f, gain));
    }
}
