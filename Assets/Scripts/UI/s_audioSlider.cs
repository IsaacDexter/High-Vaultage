using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class s_audioSlider : MonoBehaviour
{
    public AudioMixerGroup m_mixerGroup;
    
    public void SetVolume(float value)
    {
        m_mixerGroup.audioMixer.SetFloat(m_mixerGroup.name + "Volume", value);
    }
}
