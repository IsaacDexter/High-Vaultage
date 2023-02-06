using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] string LookForTag = "Player";

    public AudioSource m_audioSource;
    public AudioClip m_clip;
    public float m_volume;

    public void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_clip = m_audioSource.clip;
    }


    public void OnCollisionEnter(Collider other)
    {
        if (other.CompareTag(LookForTag))
        {
            m_audioSource.PlayOneShot(m_clip, m_volume); //Plays Pick up Sound
        }
    }
}
