using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] AudioSource m_AudioSource;
    bool canPlay;

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.V))
        {
            canPlay = true;
        }
       if (Input.GetKeyUp(KeyCode.V))
        {
            canPlay = false;
        }
       
       if(canPlay)
        {
            m_AudioSource.Play();
            canPlay = false;
        }

       if(!m_AudioSource.isPlaying)
        {
            canPlay = false;
        }
    }
}
